
Describe 'Port Check' {
    It 'The port for the webapp should be open' {
        $Port = 5050
        .\run.ps1 -Port $Port
        Start-Sleep 5 # make sure its running and ready for request

        $connectionTest = Test-NetConnection 127.0.0.1 -Port $Port
        $connectionTest.TcpTestSucceeded | Should -BeTrue
    } 
} 
# port is open - Test-NetConnection 127.0.0.1 -Port 5076
# return 200
# check log