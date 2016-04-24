Feature: ApiAuthentication
    In order to perform api operations
    As a client of the Web Api
    I want to authenticate my requests

Scenario: Get authentication token 
    When I  make a request to the api with valid credentials 
    Then my authentication has been set 

Scenario: Delete authentication token
   Given I am Api authenticated
    When I make a token delete request to the api
    Then my session is destroyed
     And my requests are now unauthorised


