# FHIRCast Websockets Implementation

## Summary

This implementation adheres to the [current FHIRCast specifications](http://fhircast.org/), and the [proposed Websocket specifications](https://github.com/HL7/fhircast-docs/wiki/Websocket-proposal). All communication from client to server will conform to WebSub and FHIRCast RESTful API specifications. Only Hub notifications to the client will be over Websockets.

This project contains a working FHIRCast Hub and test client. The Hub is implemented as a Microsoft .Net Core 2.1 console app. The test client is a .Net Framework 4.6.1 Web Forms application. 

There is no authorization used at this time. The hub will accept any topic. 

