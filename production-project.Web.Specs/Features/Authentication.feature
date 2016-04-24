Feature: Authentication
      In order be secure
      As a bed manager
      I want to login to my account

Scenario: Log in to my account
   Given I am on the login screen
    When I submit the correct credentials
    Then I am redirected to my account page
     And I am authenticated

Scenario: Log out of my account
   Given I am logged in
    When I log out
    Then I am redirected to the login page
     And I can not access an authenticated section

Scenario: Attempt to login with incorrect details
   Given I am on the login screen
    When I submit incorrect credentials
    Then I have stayed on the login screen
     And I have been shown an error alert

Scenario Outline: Attempt to login without providing username or password
   Given I am on the login screen
    When I try to submit with only <credential>
    Then I have stayed on the login screen
     And I have been shown a warning alert

Examples:
          | credential |
          | password   |
          | username   |