using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nuance.PowerCast.Common;
using Nuance.PowerCast.TestPowerCast.Properties;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static Hl7.Fhir.Model.DiagnosticReport;
using Subscription = Nuance.PowerCast.Common.Subscription;

namespace Nuance.PowerCast.TestPowerCast
{
    public partial class Form1 : Form
    {
        private ClientWebSocket _ws = null;
        private readonly WebSocketLib _webSocketLib = new WebSocketLib();
        private readonly static HttpClient _httpClient = new HttpClient();
        //private static readonly AutoResetEvent _launchCBEvent = new AutoResetEvent(false);
        private string _endpoint = null;
        private BackgroundWorker _webSocketReader;
        private readonly static HttpListener _listener = new HttpListener();
        private string _accessToken = null;
        private readonly string _connectorUrl;
        private readonly string _subscribeCallbackUrl;
        private readonly string _auth0HubAudience;
        private readonly string _auth0ClientID;
        private readonly string _auth0ClientSecret;
        private bool _authTokenOptionChecked = false;
        private ConfigurationData _config = null;
        private string _logFilePath = null;
        private HubContext _currentContext = null;
        private FhirJsonParser _fhirParser = new FhirJsonParser(new ParserSettings { AcceptUnknownMembers = true, AllowUnrecognizedEnums = true, PermissiveParsing = true });
        private FhirJsonSerializer _fhirSerializer = new FhirJsonSerializer(new SerializerSettings { Pretty = true });
        private JsonSerializerSettings _serializerSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        private bool _isSubscribed = false;

        const string APPNAME = "TestPowerCast";
        const string HUB_SECRET = "SeCreT";

        public Form1()
        {
            // put version info in title
            FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            this.Text = $"{fileVersion.ProductName} ({fileVersion.FileVersion})";
            // upgrade property settings if this is a new version
            if (fileVersion.FileVersion != Settings.Default.version)
            {
                Settings.Default.Upgrade();
                Settings.Default.Reload();
                // update the config file with new version number
                Settings.Default.version = fileVersion.FileVersion;
                Settings.Default.Save();
            }
            _connectorUrl = ConfigurationManager.AppSettings["connector_url"];
            _subscribeCallbackUrl = $"http://localhost:{ConfigurationManager.AppSettings["subscribe_callback_port"]}/";
            _listener.Prefixes.Add(_subscribeCallbackUrl);
            _auth0HubAudience = ConfigurationManager.AppSettings["auth0_hub_audience"];
            _auth0ClientID = ConfigurationManager.AppSettings["auth0_client_id"];
            _auth0ClientSecret = ConfigurationManager.AppSettings["auth0_client_secret"];
            //logging
            _logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + ConfigurationManager.AppSettings["logFilePath"];
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.File(Path.Combine(_logFilePath, "TestPowerCast.log"), shared: true)
                .CreateLogger();
            Log.Logger.Information("TestPowerCast started");
            // hide all tabs except the first tab. Show them after user gets the configuration data
            InitializeComponent();
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
            tabControl1.Deselecting += TabControl1_Deselecting;
            InitListViews();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtSubEvents.Text = Settings.Default.txtSubEvents;
            txtAccession.Text = Settings.Default.txtAccession;
            txtMRN.Text = Settings.Default.txtMRN;
            lvStudies.ItemChecked += LvStudies_ItemChecked;
            lvContent.ItemChecked += LvContent_ItemChecked;
            FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            this.Text = $"{fileVersion.ProductName} ({fileVersion.FileVersion})";
        }

        private async Task<string> GetToken()
        {
            string token = null;
            HttpResponseMessage response;
            UriBuilder urlBuilder = new UriBuilder(_config.token_endpoint);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, urlBuilder.Uri);
            Dictionary<string, string> auth = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", _auth0ClientID },
                { "client_secret", _auth0ClientSecret },
                { "audience",  _auth0HubAudience}
            };
            FormUrlEncodedContent enc = new FormUrlEncodedContent(auth);
            request.Content = enc;
            response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                Display($"Unable to obtain access token from Auth0: {(int)response.StatusCode} - {response.ReasonPhrase}", LogEventLevel.Error);
            }
            else
            {
                string s = await response.Content.ReadAsStringAsync();
                var jwt = JObject.Parse(s);
                token = jwt["access_token"].ToString();
            }
            return token;
        }

        /// <summary>
        /// Thread that reads messages from each websocket connect
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private async void webSocketReader_DoWork(object sender, DoWorkEventArgs e)
        {
            bool isUnsubscribed = false; //below while loop is listening on ws continously. Sometime, the loop try to receivedata while the await in unsubscribe is in-process.
            while (_ws.State == WebSocketState.Open && !isUnsubscribed)
            {
                Display("Websocket reader waiting for data...");
                string socketData = null;
                try
                {
                    socketData = await _webSocketLib.ReceiveStringAsync(_ws, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    Display($"Error reading websocket: {ex.Message}.", LogEventLevel.Error);
                }
                if (_ws.State != WebSocketState.Open)
                {
                    Display($"Websocket no longer open. State is {_ws.State}");
                }
                else
                {
                    int len = null == socketData ? 0 : socketData.Length;
                    Display($"{len} bytes read - processing message...");
                    Notification nMessage = JsonConvert.DeserializeObject<Notification>(socketData);
                    NotificationEvent ev = nMessage.Event;
                    if (null == ev)
                    {
                        Display($"Missing or invalid event notification received", LogEventLevel.Error);
                    }
                    else
                    {
                        Log.Information("Event notification received:\r\n{message}", nMessage);
                        Display($"Event notification received:\r\n{nMessage.ToString()}");
                        await GetContext(); // refresh our context - even if we sent the notification
                    }
                }
            }
            Display("Websocket reader terminated.");
        }

        private void UpdateListViews()
        {
            // update the list of studies
            this.lvStudies.Invoke((MethodInvoker)delegate
            {
                lvStudies.Items.Clear();
            });
            this.lvContent.Invoke((MethodInvoker)delegate
            {
                lvContent.Items.Clear();
            });

            if (null != _currentContext)
            {
                List<ContextItem> items = _currentContext.context.FindAll(c => c.Key.ToLower() == "study");
                foreach (ContextItem studyItem in items)
                {
                    ImagingStudy study = studyItem.Resource.ToImagingStudy();
                    // Create three items and three sets of subitems for each item.
                    ListViewItem item = new ListViewItem(GetAccessionNumber(study), 0);
                    item.Checked = false;
                    item.SubItems.Add(study.Status == ImagingStudy.ImagingStudyStatus.Available ? "Comparison" : "Active");
                    item.SubItems.Add(study.Started);
                    item.Tag = study;
                    this.lvStudies.Invoke((MethodInvoker)delegate
                    {
                        lvStudies.Items.Add(item);
                    });
                }
                var currentContentContextItemBundle = _currentContext.context.Find(c => c.Key.ToLower() == "content");
                if (currentContentContextItemBundle != null)
                {
                    var currentContentBundle = _fhirParser.Parse<Bundle>(JsonConvert.SerializeObject(currentContentContextItemBundle.Resource));
                    foreach (var entry in currentContentBundle.Entry)
                    {
                        entry.Resource.TryDeriveResourceType(out ResourceType resourceType);
                        if (resourceType == ResourceType.Observation)
                        {
                            ListViewItem item = new ListViewItem(entry.Resource.Id, 0);
                            item.Checked = false;
                            item.SubItems.Add(entry.Resource.TypeName);
                            item.Tag = new ContextItem { Resource = entry.Resource, Key = resourceType.ToString(), Reference = entry.FullUrl };
                            this.lvContent.Invoke((MethodInvoker)delegate
                            {
                                lvContent.Items.Add(item);
                            });
                        }
                    }

                    if (lvContent.Items == null || (lvContent.Items.Count <= 0))
                    {
                        ToggleButtonEnabled(btnUpdateContent, false);
                        ToggleButtonEnabled(btnDeleteContent, false);
                    }
                }
            }
        }

        private DiagnosticReport CurrentContextualReport
        {
            get
            {
                return (null == _currentContext ? null : _currentContext.context.Find(c => c.Key.ToLower() == "report").Resource).ToDiagnosticReport();
            }
        }

        private ImagingStudy CurrentContextualStudy
        {
            get
            {
                return (_currentContext?.context?.FirstOrDefault(c => c.Key.ToLower() == "study")?.Resource)?.ToImagingStudy();
            }
        }

        private void InitListViews()
        {
            // Set the view to show details.
            lvStudies.View = View.Details;
            // Allow the user to edit item text.
            lvStudies.LabelEdit = true;
            // Allow the user to rearrange columns.
            lvStudies.AllowColumnReorder = true;
            // Display check boxes.
            lvStudies.CheckBoxes = true;
            // Select the item and subitems when selection is made.
            lvStudies.FullRowSelect = false;
            // Display grid lines.
            lvStudies.GridLines = true;

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            lvStudies.Columns.Add("Accession Number", -2, HorizontalAlignment.Left);
            lvStudies.Columns.Add("Active/Comparison", -2, HorizontalAlignment.Left);
            lvStudies.Columns.Add("Study Date", -2, HorizontalAlignment.Left);

            // Set the view to show details.
            lvContent.View = View.Details;
            // Allow the user to edit item text.
            lvContent.LabelEdit = true;
            // Allow the user to rearrange columns.
            lvContent.AllowColumnReorder = true;
            // Display check boxes.
            lvContent.CheckBoxes = true;
            // Select the item and subitems when selection is made.
            lvContent.FullRowSelect = false;
            // Display grid lines.
            lvContent.GridLines = true;

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            lvContent.Columns.Add("Resource Id", -2, HorizontalAlignment.Left);
            lvContent.Columns.Add("Resource Type", -2, HorizontalAlignment.Left);
        }

        private string GetAccessionNumber(ImagingStudy study)
        {
            string result = null;
            foreach (Identifier i in study.Identifier)
            {
                if (null != i.Type && null != i.Type.Coding)
                {
                    foreach (Coding c in i.Type.Coding)
                    {
                        if (c.Code.ToLower() == "acsn")
                        {
                            result = i.Value;
                        }
                    }
                }
            }
            return result;
        }

        private void _callbackReader_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _listener.Start();
            }
            catch (Exception ex)
            {
                Display($"Exception occurred starting web listener: {ex.Message}", LogEventLevel.Error);
                return;
            }
            Display($"Listening for subscribe callback...");
            // The first thing we should get is an intent verification when we attempt to subscribe 
            HttpListenerContext context = _listener.GetContext();
            HttpListenerRequest request = context.Request;
            string challenge = request.QueryString["hub.challenge"];
            if (null == challenge)
            {
                Display("Intent verification failed - no challenge sent. Not subscribed.");
            }
            else
            {
                Display("Intent verification in progress. Sending back challenge...");

                // Respond with the challenge
                HttpListenerResponse response = context.Response;
                response.StatusCode = (int)HttpStatusCode.Accepted;
                var buffer = Encoding.UTF8.GetBytes(challenge);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);

                // loop here reading and processing notification messages
                System.IO.Stream body;
                System.Text.Encoding encoding;
                System.IO.StreamReader reader;
                while (true)
                {
                    try
                    {
                        context = _listener.GetContext();
                    }
                    catch (Exception ex)
                    {
                        Display($"Exception reading callback:\r\n{ex.Message}", LogEventLevel.Error);
                        break;
                    }
                    request = context.Request;
                    response = context.Response;
                    body = request.InputStream;
                    encoding = request.ContentEncoding;
                    reader = new System.IO.StreamReader(body, encoding);
                    string s = reader.ReadToEnd();
                    if (String.IsNullOrEmpty(s))
                    {
                        Display("Callback received empty message.");
                    }
                    else
                    {
                        JObject o = JObject.Parse(s);
                        string formattedJson = JsonConvert.SerializeObject(o, Formatting.Indented);
                        Display($"Callback received message:\r\n{formattedJson}");
                        Notification nMessage = null;
                        try
                        {
                            nMessage = JsonConvert.DeserializeObject<Notification>(s);
                        }
                        catch (Exception) { }
                        NotificationEvent ev = null;
                        if (null != nMessage && null != nMessage.Event)
                        {
                            ev = nMessage.Event;
                        }
                        if (null != ev)
                        {
                            Display($"Valid event notification received.");
                            response.StatusCode = (int)HttpStatusCode.Accepted;
                            string responseContent = "Accepted";
                            buffer = Encoding.UTF8.GetBytes(responseContent);
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                        else
                        {
                            Display($"Invalid event notification received.");
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            string responseContent = "BadRequest";
                            buffer = Encoding.UTF8.GetBytes(responseContent);
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                    }
                }
                Display("Callback worker thread terminated.");
            }
        }

        private void Display(string message, LogEventLevel level = LogEventLevel.Information)
        {
            if (this.InvokeRequired)
            {
                this.txtLog.Invoke((MethodInvoker)delegate
                {
                    // Running on the UI thread
                    txtLog.AppendText(DateTime.Now.ToString("G") + "\t" + message + "\r\n");
                });
            }
            else
            {
                txtLog.AppendText(DateTime.Now.ToString("G") + "\t" + message + "\r\n");
            }
            Log.Logger.Write((Serilog.Events.LogEventLevel)level, message);
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsConfigurationAvailable() &&
                (tabControl1.SelectedTab.Name != this.tabConfiguration.Name))
            {
                tabControl1.SelectedIndex = 0;
                MessageBox.Show("You must retreive or set the configuration data before performing other functions");
            }
            else if (!_isSubscribed &&
                (tabControl1.SelectedTab.Name == this.tabReportOpenClose.Name ||
                tabControl1.SelectedTab.Name == this.tabUpdateStudies.Name ||
                tabControl1.SelectedTab.Name == this.tabUpdateContent.Name))
            {
                //If we are not subscribed dont allow user to perform report actions. They need to subscribe first.
                tabControl1.SelectedIndex = 2;
                MessageBox.Show("You must first subscribe in order to perform report actions");
            }
            else if (_isSubscribed && tabControl1.SelectedTab.Name == this.tabConfiguration.Name)
            {
                //If we are subscribed dont allow the user to get configuration. They need to unsubscribe first.
                tabControl1.SelectedIndex = 2;
                MessageBox.Show("You must first unsubscribe in order to get a new topic/configuration");
            }
        }

        private void TabControl1_Deselecting(object sender, EventArgs e)
        {
            //If we are leaving the configuration tab, set the configuration values 
            if (tabControl1.SelectedTab.Name == this.tabConfiguration.Name)
            {
                if (_config == null)
                    _config = new ConfigurationData();

                _config.topic = this.txtConfigTopic.Text;
                _config.hub_endpoint = this.txtConfigHubUrl.Text;

                if (_authTokenOptionChecked)
                {
                    _accessToken = this.txtAuthToken.Text;
                }
                else
                {
                    _accessToken = null;
                    _config.token_endpoint = this.txtConfigTokenUrl.Text;
                    _config.authorization_endpoint = this.txtConfigAuthUrl.Text;
                }

                txtHubUrl.Text = _config.hub_endpoint;
                txtTopic.Text = _config.topic;
            }
        }

        private async Task<HttpResponseMessage> SendNotification(Notification notification, DiagnosticReportStatus? status = null)
        {
            if (!IsConfigurationAvailable())
                return null;

            HttpResponseMessage response = null;
            UriBuilder urlBuilder = new UriBuilder($"{txtHubUrl.Text}/{_config.topic}");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, urlBuilder.Uri);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
            // Serialize the notification and send it to the hub
            string json = JsonConvert.SerializeObject(notification, Formatting.Indented, _serializerSettings);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            Display($"Sending notification event {notification.Event.HubEvent} {(status != null ? $" with {status} status" : string.Empty)}");
            Log.Information("Sending notification:\r\n{json}", json);
            response = await SendAuthorizedRequest(request);
            return response;
        }

        private async Task<HttpResponseMessage> SendAuthorizedRequest(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (null == _accessToken)
                _accessToken = await GetToken();
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this._accessToken);
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                Display($"Exception calling web request {request}.  Message: {ex.Message}");
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.Message;
            }
            if (response.IsSuccessStatusCode)
            {
                Display("The request was accepted by the PowerCast Hub.");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && _accessToken == null)
            {
                var unauthorizedMessage = $"Received 401 response, there was a Unauthorized error. Check Auth0 config settings and try again.";
                Display(unauthorizedMessage, LogEventLevel.Error);
                MessageBox.Show(unauthorizedMessage);
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                var errMsg = await response.Content?.ReadAsStringAsync();
                var conflictMessage = $"Received 409 response. There was a conflit, use GetContext to get the latest context version. {errMsg}";
                Display(conflictMessage, LogEventLevel.Error);
                MessageBox.Show(conflictMessage);
            }
            else
            {
                string responseContent = null != response.Content ? await response.Content.ReadAsStringAsync() : "";
                string message = null;
                if (!String.IsNullOrEmpty(responseContent))
                {
                    var responseObject = new Object();
                    try { responseObject = JsonConvert.DeserializeObject(responseContent); }
                    catch { }
                    message = String.Format("The request was not accepted by the PowerCast Hub. Error code: {0} Reason: {1}", response.StatusCode, ((JObject)responseObject)["message"]);
                }
                else
                {
                    message = String.Format("The request was not accepted by the PowerCast Hub. Error code: {0}", response.StatusCode);
                }
                Display(message);
            }
            return response;
        }

        #region UI Event Handlers

        private void btnUnsubscribe_Click(object sender, EventArgs e)
        {
            SendSubscribe("unsubscribe");
            _isSubscribed = false;
            this.btnUnsubscribe.Enabled = false;
            this.btnSubscribe.Enabled = true;
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            if (IsConfigurationAvailable() && _ws != null && _ws.State == WebSocketState.Open)
            {
                var connectionMessage = $"WebSocket is already connected and subscribed.";
                Display(connectionMessage, LogEventLevel.Information);
                return;
            }

            SendSubscribe("subscribe");
            _isSubscribed = true;
            this.btnSubscribe.Enabled = false;
            this.btnUnsubscribe.Enabled = true;
        }

        private async void SendSubscribe(string mode)
        {
            if (!IsConfigurationAvailable())
                return;

            // save subscription parameters
            Settings.Default.txtSubEvents = txtSubEvents.Text;
            Settings.Default.Save();
            HttpResponseMessage response;
            //Subscribe/Unsubscribe
            UriBuilder uriBuilder = new UriBuilder($"{txtHubUrl.Text}");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Uri);
            string leaseSeconds = String.IsNullOrEmpty(txtLeaseSeconds.Text) ? "86400" : txtLeaseSeconds.Text;
            Dictionary<string, string> hub = new Dictionary<string, string>
            {
                { "hub.channel.type", "websocket"},
                { "hub.callback", ""},
                { "hub.mode",  mode },
                { "hub.topic", txtTopic.Text },
                { "hub.events", txtSubEvents.Text },
                { "hub.secret", HUB_SECRET },
                { "hub.lease_seconds", leaseSeconds },
                { "hub.subscriber", APPNAME }
            };

            if (mode != "subscribe")
            {
                hub.Add("hub.channel.endpoint", _endpoint);
            }
            FormUrlEncodedContent enc = new FormUrlEncodedContent(hub);
            request.Content = enc;
            response = await SendAuthorizedRequest(request);
            if (!response.IsSuccessStatusCode)
            {
                Display($"{btnSubscribe.Text} request was not accepted: {(int)response.StatusCode} - {response.ReasonPhrase}", LogEventLevel.Error);
            }
            else
            {
                if (mode == "unsubscribe")
                {
                    Display($"{btnSubscribe.Text} request was accepted. Closing web socket and terminating background thread.");
                    try
                    {
                        await _ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "client unsubscribing from topic", CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        Display($"Exception occurred closing the websocket: {ex.Message}", LogEventLevel.Error);
                    }
                    finally
                    {
                        _ws.Dispose();
                        _endpoint = null;
                        Display("Disposed the websocket connection");
                    }
                }
                else // SUBSCRIBE
                {
                    SubscriptionResponse subResponse = JsonConvert.DeserializeObject<SubscriptionResponse>(await response.Content.ReadAsStringAsync());
                    if (null == subResponse || null == subResponse.contexts || subResponse.contexts.Count == 0)
                    {
                        _currentContext = null;
                    }
                    else
                    {
                        _currentContext = subResponse.contexts.Find(c => c.contextType == "DiagnosticReport");
                        UpdateListViews();
                    }

                    _endpoint = subResponse.websocket_endpoint;
                    Display($"Hub returned websocket URL: {_endpoint} and contexts:\r\n{JsonConvert.SerializeObject(subResponse.contexts, Formatting.Indented)}");
                    // Connect to websocket
                    try
                    {
                        Display($"Connecting to Hub Websocket: {_endpoint}");
                        _ws = new ClientWebSocket();
                        await _ws.ConnectAsync(new Uri(_endpoint), CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        _ws.Dispose();
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                    // read the intent verification
                    Display("Expecting websocket verification. Waiting for response...");
                    string socketData;
                    try
                    {
                        socketData = await _webSocketLib.ReceiveStringAsync(_ws, CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        // Normal to get a read error thrown here when socket is closed. We'll terminate quietly
                        Display($"***** Error reading websocket intent verification: {ex.Message}.");
                        return;
                    }
                    try
                    {
                        var sub = JsonConvert.DeserializeObject<Subscription>(socketData);
                        if (sub.Topic == txtTopic.Text)
                        {
                            Display("Intent verification received. Success.");
                        }
                        else
                        {
                            Display("Intent verification failed.");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Display($"Exception parsing websocket intent verification: {ex.Message}.\r\n{socketData}");
                        return;
                    }
                    // start the websocket reader background thread
                    _webSocketReader = new BackgroundWorker();
                    _webSocketReader.DoWork += webSocketReader_DoWork;
                    _webSocketReader.RunWorkerAsync();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SetTextBox(txtLog, string.Empty);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtLog.Text))
            {
                Clipboard.SetText(txtLog.Text);
            }
        }

        private void btnNotify_Click(object sender, EventArgs e)
        {
            SendOpenNotification(true);
        }

        private void btnOpenStudy_Click(object sender, EventArgs e)
        {
            SendOpenNotification(false);
        }

        private void SendOpenNotification(bool addReportContext)
        {
            if (txtAccession.Text.Trim() == "" || txtMRN.Text.Trim() == "")
            {
                MessageBox.Show("Accession number and Patient ID (MRN) are both require to open a report.");
                return;
            }
            Settings.Default.txtAccession = txtAccession.Text;
            Settings.Default.txtMRN = txtMRN.Text;
            Settings.Default.Save();
            string[] accessionNumbers = txtAccession.Text.Split(new char[1] { ',' });
            string mrn = txtMRN.Text;
            string patientId = Guid.NewGuid().ToString("N");
            List<ContextItem> contextItems = new List<ContextItem>();
            // create the context for the notification, starting with the report
            var report = new Hl7.Fhir.Model.DiagnosticReport()
            {
                Id = Guid.NewGuid().ToString("N"),
            };
            // create and insert the Patient
            Patient patient = FhirBuilder.FhirPatient(patientId, mrn);
            contextItems.Add(new ContextItem
            {
                Key = "patient",
                Resource = patient
            });
            // add the study/studies to the context
            foreach (string acsn in accessionNumbers)
            {
                var study = FhirBuilder.FhirStudy(Guid.NewGuid().ToString("N"), acsn, patient.Id);
                contextItems.Add(new ContextItem()
                {
                    Key = "study",
                    Resource = study
                });

                report.ImagingStudy.Add(new ResourceReference
                {
                    Reference = $"ImagingStudy/{study.Id}"
                });
            }

            if (addReportContext)
            {
                contextItems.Add(new ContextItem()
                {
                    Key = "report",
                    Resource = report
                });
            }

            // create and send the notification for DiagnosticReport-open event
            Notification notification = new Notification
            {
                Timestamp = DateTime.Now,
                Id = $"{APPNAME}-{Guid.NewGuid():N}",
                Event = new NotificationEvent()
                {
                    HubEvent = addReportContext ? "DiagnosticReport-open" : "ImagingStudy-open",
                    Topic = _config.topic,
                    Context = contextItems
                }
            };
            _ = SendNotification(notification);
        }

        private async void btnNotifyError_Click(object sender, EventArgs e)
        {
            Notification notification = GetSyncErrorNotification();

            var response = await SendNotification(notification);

            if (response != null && !response.IsSuccessStatusCode)
            {
                Display($"Error sending notification: {(int)response.StatusCode} - {response.ReasonPhrase}", LogEventLevel.Error);
            }
        }

        private async void btnGetConfig_Click(object sender, EventArgs e)
        {
            Display("Getting configuration data...");
            HttpResponseMessage response = null;
            UriBuilder urlBuilder = new UriBuilder($"{_connectorUrl}/configuration");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlBuilder.Uri);
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException && ex.InnerException.GetType() == typeof(WebException) && ((WebException)ex.InnerException).Status == WebExceptionStatus.ConnectFailure)
                {
                    MessageBox.Show("A network connection error occurred. Make sure that the PowerCast Connector program is started first.");
                }
                else
                {
                    Display($"Exception occurred getting configuration data: {ex.Message} See log file for details.");
                    Log.Error(ex, "Exception attempting to get configuration data from Connector.");
                }
            }
            if (null != response)
            {
                if (!response.IsSuccessStatusCode)
                {
                    Display($"***** The request to get configuration data was not accepted: {(int)response.StatusCode} - {response.ReasonPhrase}");
                }
                else
                {
                    Display("Configuration data received.");
                    string configJson = await response.Content.ReadAsStringAsync();
                    _config = JsonConvert.DeserializeObject<ConfigurationData>(configJson);
                    txtConfigAuthUrl.Text = _config.authorization_endpoint;
                    txtConfigHubUrl.Text = _config.hub_endpoint;
                    txtConfigTokenUrl.Text = _config.token_endpoint;
                    txtConfigTopic.Text = _config.topic;
                    txtHubUrl.Text = _config.hub_endpoint;
                    txtTopic.Text = _config.topic;
                    btnDownloadHubLogs.Enabled = true;
                }
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            Display("Logging in to PowerScribe...");
            HttpResponseMessage response;
            UriBuilder urlBuilder = new UriBuilder($"{_connectorUrl}/login?username={txtLaunchUsername.Text}");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlBuilder.Uri);
            response = await SendAuthorizedRequest(request);
            if (!response.IsSuccessStatusCode)
            {
                Display($"***** The login request was not accepted: {(int)response.StatusCode} - {response.ReasonPhrase}");
            }
            else
            {
                string configJson = await response.Content.ReadAsStringAsync();
                _config = JsonConvert.DeserializeObject<ConfigurationData>(configJson);
                txtConfigAuthUrl.Text = _config.authorization_endpoint;
                txtConfigHubUrl.Text = _config.hub_endpoint;
                txtConfigTokenUrl.Text = _config.token_endpoint;
                txtConfigTopic.Text = _config.topic;
                txtTopic.Text = _config.topic;
                Display($"Login completed. Using topic {_config.topic}.");
            }
        }

        private void btnCloseReport_Click(object sender, EventArgs e)
        {
            SendCloseNotification(DiagnosticReportStatus.Partial);
        }

        private void btnSignReport_Click(object sender, EventArgs e)
        {
            SendCloseNotification(DiagnosticReportStatus.Final);
        }

        private void btnPrelimReport_Click(object sender, EventArgs e)
        {
            SendCloseNotification(DiagnosticReportStatus.Preliminary);
        }

        private async void btnGetReport_Click(object sender, EventArgs e)
        {
            await GetContext();
            Display($"New context:\r\n{JsonConvert.SerializeObject(_currentContext, Formatting.Indented, _serializerSettings)}");
        }

        private async Task<bool> GetContext()
        {
            if (!IsConfigurationAvailable())
                return false;

            string hubUrl = txtHubUrl.Text;
            var request = new HttpRequestMessage(HttpMethod.Get, $"{hubUrl}/{_config.topic}");
            HttpResponseMessage response = null;
            try
            {
                response = await SendAuthorizedRequest(request);
                if (!response.IsSuccessStatusCode)
                {
                    Display($"PowerCast GetTopic request was not accepted: {(int)response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Display($"Exception connecting to the hub: {hubUrl} \r\nRequest: {request} \r\nException: {ex.Message}");
            }
            string contextJson = await response.Content.ReadAsStringAsync();
            var contexts = JsonConvert.DeserializeObject<List<HubContext>>(contextJson);
            if (null == contexts || contexts.Count == 0)
            {
                _currentContext = null;
            }
            else
            {
                _currentContext = contexts.Find(c => c.contextType == "DiagnosticReport");
            }
            UpdateListViews();
            return true;
        }

        /// <summary>
        /// Set the version in the notification
        /// </summary>
        private void SetContextVersion(Notification notification)
        {
            if (notification != null && notification.Event != null)
            {
                notification.Event.ContextVersionId = _currentContext.contextVersionId;
            }
        }

        private async void btnDownloadHubLogs_Click(object sender, EventArgs e)
        {
            Display("Getting Hub log files...");
            HttpResponseMessage response;
            string hubUrl = _config.hub_endpoint;
            UriBuilder urlBuilder = new UriBuilder(hubUrl.Substring(0, hubUrl.LastIndexOf("/")) + "/log/file");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlBuilder.Uri);
            response = await SendAuthorizedRequest(request);
            if (!response.IsSuccessStatusCode)
            {
                Display($"***** The request to get hub log file was not accepted: {(int)response.StatusCode} - {response.ReasonPhrase}");
            }
            else
            {
                Display("Hub log file received.");
                string fileData = await response.Content.ReadAsStringAsync();
                saveFileDialog1.Filter = "Log File|*.log|Text File|*.txt";
                saveFileDialog1.FileName = "fhir-log-" + DateTime.Now.ToString("yyyy_MMdd_hhmm");
                saveFileDialog1.DefaultExt = "log";
                saveFileDialog1.Title = "Save the Hub Log File";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    File.WriteAllText(saveFileDialog1.FileName, fileData);
                }
            }
        }

        private void btnOpenLogFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", _logFilePath);
        }

        private void LvStudies_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            btnUpdateStudy.Enabled = lvStudies.CheckedIndices.Count > 0;
            btnDeleteStudy.Enabled = lvStudies.CheckedIndices.Count > 0;
        }

        private void LvContent_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            // enable/disable update and delete buttons
            bool itemChecked = lvContent.CheckedIndices.Count > 0;
            ToggleButtonEnabled(btnUpdateContent, itemChecked);
            ToggleButtonEnabled(btnDeleteContent, itemChecked);

            // if the selected item has a reference to the resource, enable the "load references button"
            if (itemChecked)
            {
                ListViewItem lvItem = lvContent.Items[lvContent.CheckedIndices[0]];
                object oTag = lvItem.Tag;
                ContextItem context = (ContextItem)oTag;
                ToggleButtonEnabled(btnLoadReference, (context.Reference != null));
            }
            else
            {
                ToggleButtonEnabled(btnLoadReference, false);
            }
        }

        private async void btnAddStudy_Click(object sender, EventArgs e)
        {
            if (null == _currentContext)
            {
                MessageBox.Show("There is no report opened in PowerScribe.");
                return;
            }
            AddStudyForm form = new AddStudyForm();
            DialogResult result = form.ShowDialog();
            DiagnosticReport currentReport = CurrentContextualReport;
            if (result == DialogResult.OK)
            {
                ImagingStudy study = null;
                try
                {
                    study = _fhirParser.Parse<ImagingStudy>(form.studyJson);
                }
                catch (Exception ex)
                {
                    Display("Exception occurred parsing Study JSON: " + ex.Message);
                    return;
                }
                List<Bundle.EntryComponent> entries = new List<Bundle.EntryComponent>();
                entries.Add(new Bundle.EntryComponent
                {
                    Request = new Bundle.RequestComponent()
                    {
                        Method = Bundle.HTTPVerb.POST
                    },
                    Resource = study
                });
                Notification notification = new Notification
                {
                    Timestamp = DateTime.Now,
                    Id = $"{APPNAME}-{Guid.NewGuid():N}",
                    Event = new NotificationEvent()
                    {
                        HubEvent = "DiagnosticReport-update",
                        Topic = _config.topic,
                        Context = new List<ContextItem>
                        {
                            new ContextItem()
                            {
                                Key = "report",
                                Resource = new DiagnosticReport
                                {
                                    Id = currentReport.Id,
                                    Meta = currentReport.Meta
                                }
                            },
                            new ContextItem()
                            {
                                Key = "updates",
                                Resource = new Bundle
                                {
                                    Id = Guid.NewGuid().ToString("N"),
                                    Type = Bundle.BundleType.Transaction,
                                    Entry = entries
                                }
                            }
                        }
                    }
                };
                SetContextVersion(notification);

                // send the notification event to the hub
                var response = await SendNotification(notification);
                if (!response.IsSuccessStatusCode)
                {
                    Display($"Error sending notification: {(int)response.StatusCode} - {response.ReasonPhrase}", LogEventLevel.Error);
                }
            }
        }

        private void btnUpdateStudy_Click(object sender, EventArgs e)
        {
            UpdateStudy(Bundle.HTTPVerb.PUT);
        }

        private void btnDeleteStudy_Click(object sender, EventArgs e)
        {
            UpdateStudy(Bundle.HTTPVerb.DELETE);
        }

        private void UpdateStudy(Bundle.HTTPVerb verb)
        {
            DiagnosticReport currentReport = CurrentContextualReport;
            Bundle bundle = new Bundle
            {
                Id = Guid.NewGuid().ToString("N"),
                Type = Bundle.BundleType.Transaction,
                Entry = new List<Bundle.EntryComponent>()
            };
            ImagingStudy study = null;
            foreach (ListViewItem studyItem in lvStudies.CheckedItems)
            {
                study = (ImagingStudy)studyItem.Tag;
                break; // there should only be one because multiselect == false
            }
            Bundle.EntryComponent entry = new Bundle.EntryComponent
            {
                Request = new Bundle.RequestComponent() { Method = verb },
                Resource = study
            };
            bundle.Entry.Add(entry);
            if (verb == Bundle.HTTPVerb.PUT)
            {
                // create the json and let the user change it
                string studyJson = _fhirSerializer.SerializeToString(bundle);
                var sendForm = new SendForm(studyJson);
                DialogResult result = sendForm.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }
                else
                {
                    try
                    {
                        bundle = _fhirParser.Parse<Bundle>(sendForm.FinalJson);
                        bundle.Entry.First().Request.Method = verb; // make sure the user didn't change this
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error parsing Bundle Json: {ex.Message}");
                        return;
                    }
                }
            }

            Notification notification = new Notification
            {
                Timestamp = DateTime.Now,
                Id = $"{APPNAME}-{Guid.NewGuid():N}",
                Event = new NotificationEvent()
                {
                    HubEvent = "DiagnosticReport-update",
                    Topic = _config.topic,
                    Context = new List<ContextItem>
                    {
                        new ContextItem()
                        {
                            Key = "report",
                            Resource = new DiagnosticReport()
                            {
                                Id = currentReport.Id,
                                Meta = new Meta()
                                {
                                    VersionId = currentReport.Meta.VersionId
                                }
                            }
                        },
                        new ContextItem()
                        {
                            Key = "updates",
                            Resource = bundle
                        }
                    }
                }
            };
            SetContextVersion(notification);

            _ = SendNotification(notification);
        }

        private void btnAddContent_Click(object sender, EventArgs e)
        {
            if (null == _currentContext)
            {
                MessageBox.Show("There is no report opened in PowerScribe.");
                return;
            }
            // open file(s) containing json FHIR resources
            openFileDialog1.InitialDirectory = $"{Directory.GetCurrentDirectory()}\\content";
            openFileDialog1.Filter = "Json File|*.json|Text File|*.txt";
            openFileDialog1.DefaultExt = "json";
            openFileDialog1.Title = "Json File";
            openFileDialog1.FileName = "";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Read the json from the selected files and insert it into a bundle.
                var bundle = GenerateObservationBundle(openFileDialog1.FileNames.ToList());
                var sendForm = new SendForm(_fhirSerializer.SerializeToString(bundle));

                DialogResult result = sendForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        bundle = _fhirParser.Parse<Bundle>(sendForm.FinalJson);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error parsing Bundle Json: {ex.Message}");
                        return;
                    }
                    Notification notification = new Notification
                    {
                        Timestamp = DateTime.Now,
                        Id = $"{APPNAME}-{Guid.NewGuid():N}",
                        Event = new NotificationEvent()
                        {
                            HubEvent = "DiagnosticReport-update",
                            Topic = _config.topic,
                            Context = CreateContext(bundle)
                        }
                    };
                    SetContextVersion(notification);

                    _ = SendNotification(notification);
                }
            }
        }

        private Bundle GenerateObservationBundle(List<string> fileNames)
        {
            var bundleEntries = new List<Bundle.EntryComponent>();
            try
            {
                foreach (var filename in fileNames)
                {
                    string currentResourceJson = File.ReadAllText(filename);

                    //Check if we encountered a jarray, if so add each resource from the array to our bundle
                    var resourceJToken = JToken.Parse(currentResourceJson);
                    if (resourceJToken is JArray)
                    {
                        var resourceArray = resourceJToken as JArray;
                        foreach (var currentResource in resourceArray.Children())
                        {
                            var currentBundleEntry = new Bundle.EntryComponent
                            {
                                Request = new Bundle.RequestComponent { Method = Bundle.HTTPVerb.POST },
                                Resource = _fhirParser.Parse<DomainResource>(currentResource.ToString())
                            };
                            bundleEntries.Add(currentBundleEntry);
                        }
                    }
                    else
                    {
                        //Just one obervation resource in the file so parse and add it to our bundle
                        var currentBundleEntry = new Bundle.EntryComponent
                        {
                            Request = new Bundle.RequestComponent { Method = Bundle.HTTPVerb.POST },
                            Resource = _fhirParser.Parse<DomainResource>(currentResourceJson)
                        };
                        bundleEntries.Add(currentBundleEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing Json: {ex.Message}");
                return null;
            }

            Bundle bundle = new Bundle
            {
                Id = Guid.NewGuid().ToString("N"),
                Type = Bundle.BundleType.Transaction,
                Entry = bundleEntries
            };

            return bundle;
        }

        private void UpdateContent(Bundle.HTTPVerb verb)
        {
            bool sendUpdate = true;
            Bundle bundle = new Bundle
            {
                Id = Guid.NewGuid().ToString("N"),
                Type = Bundle.BundleType.Transaction,
                Entry = new List<Bundle.EntryComponent>()
            };
            DiagnosticReport currentReport = CurrentContextualReport;
            object resource = null;
            string resourceType = null;
            foreach (ListViewItem contentItem in lvContent.CheckedItems)
            {
                ContextItem context = (ContextItem)contentItem.Tag;
                resource = context.Resource;
                resourceType = ((JObject)resource)["resourceType"].ToString();
                break; // there should only be one because multiselect == false
            }
            Bundle.EntryComponent entry = new Bundle.EntryComponent
            {
                Request = new Bundle.RequestComponent() { Method = verb },
            };
            switch (resourceType.ToLower())
            {
                case "observation":
                    entry.Resource = _fhirParser.Parse<Observation>(JsonConvert.SerializeObject(resource));
                    break;
                case "media":
                    entry.Resource = _fhirParser.Parse<Media>(JsonConvert.SerializeObject(resource));
                    break;
            }
            bundle.Entry.Add(entry);
            if (verb == Bundle.HTTPVerb.PUT)
            {
                var sendForm = new SendForm(_fhirSerializer.SerializeToString(bundle));
                DialogResult result = sendForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        bundle = _fhirParser.Parse<Bundle>(sendForm.FinalJson);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error parsing Bundle Json: {ex.Message}");
                        return;
                    }
                }
                else
                {
                    sendUpdate = false;
                }
            }

            if (sendUpdate)
            {
                Notification notification = new Notification
                {
                    Timestamp = DateTime.Now,
                    Id = $"{APPNAME}-{Guid.NewGuid():N}",
                    Event = new NotificationEvent()
                    {
                        HubEvent = "DiagnosticReport-update",
                        Topic = _config.topic,
                        Context = CreateContext(bundle)
                    }
                };

                SetContextVersion(notification);

                _ = SendNotification(notification);
            }
        }

        private void btnUpdateContent_Click(object sender, EventArgs e)
        {
            if (null == _currentContext)
            {
                MessageBox.Show("There is no report opened in PowerScribe.");
                return;
            }
            UpdateContent(Bundle.HTTPVerb.PUT);
        }

        private void btnDeleteContent_Click(object sender, EventArgs e)
        {
            if (null == _currentContext)
            {
                MessageBox.Show("There is no report opened in PowerScribe.");
                return;
            }
            UpdateContent(Bundle.HTTPVerb.DELETE);
        }

        private void btnUserLogout_Click(object sender, EventArgs e)
        {
            if (!IsConfigurationAvailable())
            {
                return;
            }
            else if (_ws == null || _ws.State != WebSocketState.Open)
            {
                //handle edge case if user click logout after getting topic but before subscribing
                ResetTopic();
                return;
            }

            // send userLogout event
            Notification notification = new Notification
            {
                Timestamp = DateTime.Now,
                Id = $"{APPNAME}-{Guid.NewGuid():N}",
                Event = new NotificationEvent()
                {
                    HubEvent = "userlogout",
                    Topic = _config.topic,
                    Context = new List<ContextItem>()
                }
            };
            _ = SendNotification(notification);
        }
        private async void btnLoadReference_Click(object sender, EventArgs e)
        {
            ContextItem item = ((ContextItem)lvContent.Items[lvContent.CheckedIndices[0]].Tag);
            HttpResponseMessage response = null;
            UriBuilder urlBuilder = new UriBuilder($"{txtHubUrl.Text}/{_config.topic}/resource/{item.Reference}");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlBuilder.Uri);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
            Display($"Getting FHIR resource from {urlBuilder.Uri.ToString()}");
            response = await SendAuthorizedRequest(request);
            string resourceJson = await response.Content.ReadAsStringAsync();
            if (String.IsNullOrEmpty(resourceJson))
            {
                MessageBox.Show("Unable to read FHIR resource.");
            }
            else
            {
                string resourceType = ((JObject)item.Resource)["resourceType"].ToString();
                switch (resourceType.ToLower())
                {
                    case "media":
                        Media media = _fhirParser.Parse<Media>(resourceJson);
                        string contentType = media.Content.ContentType;
                        Display($"Received Media resource with content type {contentType}");
                        Log.Information($"Received Media resource:\r\n{media.ToJson(new FhirJsonSerializationSettings { Pretty = true })}");
                        // display the content
                        if (contentType == "image/jpeg")
                        {
                            ImageForm imageForm = new ImageForm();
                            imageForm.LoadImage(media.Content.Data);
                            imageForm.ShowDialog();
                        }
                        break;
                        // Some day other resources may go here...
                }
            }
        }

        private Notification GetSyncErrorNotification()
        {
            var issue = new List<OperationOutcome.IssueComponent>
            {
                new OperationOutcome.IssueComponent
                {
                    Severity = OperationOutcome.IssueSeverity.Error,
                    Code = OperationOutcome.IssueType.Processing,
                    Diagnostics = "Message that need to be shown to the user. This could include all necessary report details like acc & mrn, and how to triage.",
                    Details = new CodeableConcept
                    {
                        Coding = new List<Coding>
                        {
                            new Coding
                            {
                                System = "https://fhircast.hl7.org/events/syncerror/eventid",
                                Code = Guid.NewGuid().ToString()
                            },
                            new Coding
                            {
                                System = "https://fhircast.hl7.org/events/syncerror/eventname",
                                Code = "study-save"
                            },
                            new Coding
                            {
                                System = "https://fhircast.hl7.org/events/syncerror/vendorname",
                                Code = "testVendor"
                            },
                            new Coding
                            {
                                System = "http://example.com/events/syncerror/errorcode",
                                Code = "500 - Internal Server Error",
                            }
                        }
                    }
                }
            };

            var contextItems = new List<ContextItem>
            {
                new ContextItem
                {
                    Key = ResourceType.OperationOutcome.ToString(),
                    Resource = new OperationOutcome
                    {
                        Issue = issue
                    }
                }
            };

            // create and send the notification for syncerror event
            return new Notification
            {
                Timestamp = DateTime.Now,
                Id = $"{APPNAME}-{Guid.NewGuid():N}",
                Event = new NotificationEvent()
                {
                    HubEvent = "syncerror",
                    Topic = _config.topic,
                    Context = contextItems
                }
            };
        }

        private void SendCloseNotification(DiagnosticReportStatus status)
        {
            if (null == _currentContext)
            {
                MessageBox.Show("There is no report open in PowerScribe.");
                return;
            }
            // send DiagnosticReport-close event
            Notification notification = new Notification
            {
                Timestamp = DateTime.Now,
                Id = $"{APPNAME}-{Guid.NewGuid():N}",
                Event = new NotificationEvent()
                {
                    HubEvent = "DiagnosticReport-close",
                    Topic = _config.topic,
                    Context = new List<ContextItem>
                    {
                        new ContextItem()
                        {
                            Key = "report",
                            Resource = new DiagnosticReport
                            {
                                Id = CurrentContextualReport.Id,
                                Status = status
                            }
                        }
                    }
                }
            };
            _ = SendNotification(notification, status);
        }

        private void btnCloseStudy_Click(object sender, EventArgs e)
        {
            if (null == _currentContext)
            {
                MessageBox.Show("There is no report open in PowerScribe.");
                return;
            }
            // send DiagnosticReport-close event
            Notification notification = new Notification
            {
                Timestamp = DateTime.Now,
                Id = $"{APPNAME}-{Guid.NewGuid():N}",
                Event = new NotificationEvent()
                {
                    HubEvent = "ImagingStudy-close",
                    Topic = _config.topic,
                    Context = new List<ContextItem>
                    {
                        new ContextItem()
                        {
                            Key = "study",
                            Resource = new ImagingStudy
                            {
                                Id = CurrentContextualStudy?.Id
                            }
                        }
                    }
                }
            };
            _ = SendNotification(notification, null);
        }

        #endregion

        private void ResetTopic()
        {
            if (_config != null)
                _config.topic = null;
            SetTextBox(txtTopic);
            SetTextBox(txtConfigTopic);
            Display($"Resetting topic from configuration.");
        }

        private bool IsConfigurationAvailable()
        {
            var configAvailable = _config != null &&
                !(string.IsNullOrEmpty(_config.topic) ||
                string.IsNullOrEmpty(_config.hub_endpoint));

            if (!configAvailable)
                return false;

            if (!_authTokenOptionChecked)
            {
                configAvailable = !(string.IsNullOrEmpty(_config.authorization_endpoint) ||
                    string.IsNullOrEmpty(_config.token_endpoint));
            }
            else if (string.IsNullOrWhiteSpace(_accessToken))
            {
                return false;
            }

            return configAvailable;
        }

        /// <summary>
        /// Set TextBox values thread safe
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="value"></param>
        public void SetTextBox(TextBox textBox, string value = null)
        {
            if (textBox.InvokeRequired == true)
            {
                Action safeWrite = delegate { SetTextBox(textBox, value); };
                textBox.BeginInvoke(safeWrite);
            }
            else
            {
                textBox.Text = value;
            }
        }

        /// <summary>
        /// Set button enabled thread safe
        /// </summary>
        /// <param name="button"></param>
        /// <param name="value"></param>
        public void ToggleButtonEnabled(Button button, bool value)
        {
            if (button.InvokeRequired == true)
            {
                Action safeWrite = delegate { ToggleButtonEnabled(button, value); };
                button.BeginInvoke(safeWrite);
            }
            else
            {
                button.Enabled = value;
            }
        }

        private List<ContextItem> CreateContext(Bundle bundle)
        {
            List<ContextItem> context;

            if (cbxIncludeCurrentContext.Checked)
            {
                context = new List<ContextItem>(_currentContext.context)
                {
                    new ContextItem()
                    {
                        Key = "content",
                        Resource = bundle
                    }
                };
            }
            else
            {
                context = new List<ContextItem>
                {
                    new ContextItem()
                    {
                        Key = "report",
                        Resource = new DiagnosticReport()
                        {
                            Id= CurrentContextualReport.Id,
                            Meta = CurrentContextualReport.Meta
                        }
                    },
                    new ContextItem()
                    {
                        Key = "content",
                        Resource = bundle
                    }
                };
            }

            return context;
        }

        /// <summary>
        /// Gets a list of subscriptions for the current topic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonGetSubscriptions_Click(object sender, EventArgs e)
        {
            if (!IsConfigurationAvailable())
                return;

            string hubUrl = txtHubUrl.Text.Trim();
            string encodedEndpoint = HttpUtility.UrlEncode(_endpoint);
            var request = new HttpRequestMessage(HttpMethod.Get, $"{hubUrl}/TopicSubscriptions/{_config.topic}/{encodedEndpoint}");
            HttpResponseMessage response = null;

            try
            {
                response = await SendAuthorizedRequest(request);
                if (!response.IsSuccessStatusCode)
                {
                    Display($"PowerCast GetSubscriptions request was not accepted: {(int)response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Display($"Exception connecting to the hub: {hubUrl} \r\nRequest: {request} \r\nException: {ex.Message}");
            }

            string subscriptionJson = await response.Content.ReadAsStringAsync();
            var subscriptions = JsonConvert.DeserializeObject(String.IsNullOrWhiteSpace(subscriptionJson) ? String.Empty : subscriptionJson);

            Display($"Subscription(s) in the topic '{_config.topic}' are below:\r\n{JsonConvert.SerializeObject(subscriptions, Formatting.Indented, _serializerSettings)}");
        }

        private void useAuthToken_CheckedChanged(object sender, EventArgs e)
        {
            _authTokenOptionChecked = this.useAuthToken.Checked;
            this.btnGetConfig.Enabled = !_authTokenOptionChecked;

            //If checked hide/disable the auth0 text boxes and labels
            this.txtConfigAuthUrl.Visible = !_authTokenOptionChecked;
            this.txtConfigAuthUrl.Enabled = !_authTokenOptionChecked;
            this.txtConfigTokenUrl.Visible = !_authTokenOptionChecked;
            this.txtConfigTokenUrl.Enabled = !_authTokenOptionChecked;
            this.label2.Visible = !_authTokenOptionChecked;
            this.label8.Visible = !_authTokenOptionChecked;

            //If checked display/enable auth token textbox and label
            this.txtAuthToken.Visible = _authTokenOptionChecked;
            this.txtAuthToken.Enabled = _authTokenOptionChecked;
            this.lblAuthToken.Visible = _authTokenOptionChecked;
        }
    }
}
