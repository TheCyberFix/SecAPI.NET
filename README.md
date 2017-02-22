# SecAPI.NET

A collection of simple APIs for security products initally created to respond to new IOCs at 'machine speed.'


## Requirements to Build
- .NET  4.5.2+
- Nuget Package RestSharp



## Cisco AMP for Endpoints 
- A basic API Client for Cisco AMP for Endpoints API v1.  See https://api-docs.amp.cisco.com/

Example:
```
using SecAPI;
using SecAPI.Models;

var x = new AMPClientv1("https://api.amp.cisco.com/v1/", ampAPIun, ampAPIkey);

//Pulls wealth of current host information including current IPs/MACs, OS, etc.
CiscoAMPEndpointsv1.RootObject rootObj = x.getComputerByHostname("host", 10);

//Powerful retrospective quarantine
x.postSHAtoFileList(ampCustomFileListGUID, sha256);
```



## Proofpoint TAP API
- A basic API Client for ProofPoint TAP API v2.  See https://help.proofpoint.com/Threat_Insight_Dashboard/API_Documentation

Example:
```
using SecAPI;
using SecAPI.Models;

var x = new PPTAPClientv2("https://tap-api-v2.proofpoint.com/v2/", PPServicePrincipal, PPSecret );

//Get email message events
var events = x.getEvents();

//Forensic data related to threat and campaign
var rept = x.getForensics(sha256, true);

```



## Regards

- https://github.com/restsharp/RestSharp
- http://json2csharp.com/
- https://github.com/Genbox/VirusTotal.NET