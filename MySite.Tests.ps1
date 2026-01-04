BeforeAll {
        $Port = 5050
        .\run.ps1 -Port $Port
        Start-Sleep 5 # make sure its running and ready for request
}

Describe 'Connection Check' {
    It 'Port is open' {
        $connectionTest = Test-NetConnection 127.0.0.1 -Port $Port
        $connectionTest.TcpTestSucceeded | Should -BeTrue
    } 

    It "/health returns 200" {
    $response = Invoke-WebRequest "http://127.0.0.1:$Port/health"
    $response.StatusCode | Should -Be 200
    }
} 

Describe 'Logging' {
    It 'Log file exists'{
        Test-Path "MySite/logs/app.log" | Should -BeTrue
    }

    It 'Log contains get requestt'{
        'MySite/logs/app.log' | Should -FileContentMatch 'the /health endpoint was called'
    }
}