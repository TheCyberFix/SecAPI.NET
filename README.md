# SecAPI.NET

A collection of simple APIs for security products initally created to respond to new IOCs at 'machine speed.'


## Requirements to Build
- .NET  4.5.2+
- Nuget Package RestSharp
- Windows 10+/Server 2016+ (Only for AMSI Client)



## Cisco AMP for Endpoints 
- A basic API Client for Cisco AMP for Endpoints API v1.  See https://api-docs.amp.cisco.com/

Example:
```
using SecAPI;
using SecAPI.Models;

var x = new CiscoAMPClientv1("https://api.amp.cisco.com/v1/", ampAPIun, ampAPIkey);

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

var x = new ProofpointTAPClientv2("https://tap-api-v2.proofpoint.com/v2/", PPServicePrincipal, PPSecret );

//Get email message (SIEM) events
var events = x.getEvents();

//Forensic data related to threat and campaign
var rept = x.getForensics(sha256, true);

//Campaign data
var campRtp = x.getCampaign(campaignID);

```



## Microsoft Antimalware Scan Interface (AMSI)
- A basic *Client* for AMSI v10_0_14393_0.  See https://msdn.microsoft.com/en-us/library/windows/desktop/dn889587(v=vs.85).aspx
- Thanks to Adam Driscoll for a PowerShell/.NET implementation: https://github.com/adamdriscoll/AMSI

Example:
```

using (var x = new MicrosoftAMSIClientv10_0_14393_0("TestApp"))
{
	//Scan a string
	Console.WriteLine(x.scanString(@"X5O!P%@AP[4\PZX54(P^)7CC)7}$EICAR-STANDARD-ANTIVIRUS-TEST-FILE!$H+H*", "meaningfulContentName"));
	// Output: AMSI_RESULT_DETECTED
	
	//Scan a Byte Array
	var buff = System.Text.Encoding.Unicode.GetBytes(@"not a virus!");
	Console.WriteLine(x.scanByteArray(buff, "meaningfulContentName"));
	// Output: AMSI_RESULT_NOT_DETECTED

}

//Go look in Windows Defender History for 'All Detected Items' (or currently registered Antimalware Program that uses AMSI)

```





## Regards

- https://github.com/restsharp/RestSharp
- http://json2csharp.com/
- https://github.com/Genbox/VirusTotal.NET
- https://github.com/adamdriscoll/AMSI