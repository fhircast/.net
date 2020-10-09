# FHIRCast Websockets Implementation - Nuance PowerCast client application

## Summary

This implementation adheres to the [current FHIRCast STU2 DRAFT specifications](http://fhircast.org/specification/STU2/)

This project contains a working FHIRCast (Nuance PowerCast implementation) test client. 

## Authentication (OAuth2)

Before building and running the project, edit the app.config file to enter your
Auth0 client ID and secret. These authentication credentials can only be obtained through
Nuance Communications Inc.

Example:

  <appSettings>
    <add key="auth0_domain" value="https://nuancehdp.auth0.com/" />
    <add key="auth0_client_id" value="sdfklj34905kseihj923" />
    <add key="auth0_client_secret" value="xcv0985kjlasdjfkwerh" />
    <add key="auth0_hub_audience" value="https://nuancehdp.com/PowerCast/Hub" />
    <add key="connector_url" value="http://localhost:9292" />
    <add key="subscribe_callback_port" value="8181" />
	<add key="logFilePath" value="\Nuance\PowerScribeOne\Logs\" />
  </appSettings>
