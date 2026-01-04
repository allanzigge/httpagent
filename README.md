### Run

The web app can be run as a background process with default or custom port by running the ```run.ps1``` file, eg:
```.\run.ps1``` default port is 5050
 ```.\run.ps1 -Port 7777``` or whatever port you'd like

I used netstat to manually check:
```netstat -ano | findstr :5050```


The logfile is located in HTTPAGENT/MySite/logs/app.log. 
Note that logfile does not exist in the github repo.
To make sure the log is created automatically, move or delete an existing log and run the app. 

## Automation 
- I observed that no ```winget``` was needed for running scrips or the app itself. 
- A workflow build and runs the one Nunit test. 
- At the moment the workflow runs with port 5050, but can be changed swiftly.
- Pester test succeeds locally, but it seems the connection fails when running on Github Workflows - which is an issue i have not solved.
I did test that Pester actually runs a test successfully, which is the ```Dummy.Tests.ps1``` file.
