Feature: Case of getting health checks

Scenario: Get liveness health check
	Given I am not connected
	When I request liveness health check
	Then a success is returned with an HTTP status 200

Scenario: Get readiness health check
	Given I am not connected
	When I request readiness health check
	Then a success is returned with an HTTP status 200
