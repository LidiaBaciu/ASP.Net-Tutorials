# SecureAPI
 https://www.youtube.com/watch?v=3PyUjOmuFic

# Development
- Created the application by using the command dotnet new webapi -n SecureAPI
- Registered on portal.azure.com and registered an app named WeatherAPI_Development and then exposed the API to retrieve the resource id.
- Updated the Manifest file. Changed the appRoles value to:
	"appRoles": [ 
	  { 
		"allowedMemberTypes": [ 
		  "Application" 
		], 
		"description": "Daemon apps in this role can consume the web api.",
		"displayName": "DaemonAppRole",
		"id": "6543b78e-0f43-4fe9-bf84-0ce8b74c06a3",
		"isEnabled": true,
		"lang": null,
		"origin": "Application",
		"value": "DaemonAppRole"
	  } 
	]
- Connected the .NET application to the Azure through the appsettings.json file
- Created the WeatherClient_Development application to the Azure
- Created a client secret (application password) BGe~mLE5~FCdonIwcpJQUL5J6D.6~9Qu~.
- Created the client application and added the properties