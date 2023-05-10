Feature: SL_Signin

In order to be able to buy items
As a registered user of the Sauce Labs website 
I want to be able to sign in to my account

@Signin
@Happy
Scenario: Login with valid email and valid password
    Given I am on the home page 
    And I have entered a valid user name
    And I have entered a valid password
    When I click the login button 
    Then I should land on the inventory page

@Signin
@Sad
Scenario: Login with valid email and invalid password
    Given I am on the home page 
    And I have entered a valid user name
    And I have entered a invalid "<passwords>"
    When I click the login button 
    Then I should see an error message that contains "Epic sadface"
Examples: 
| passwords |
| wrong     |
| 12345     |
| Nishy     |

@Signin
@Sad
Scenario: Login with invalid email and invalid password
    Given I am on the home page 
    And I have the following credentials
    | Username     | Password |
    | fakeusername | nish     |
    And I input these credentials
    When I click the login button 
    Then I should see an error message that contains "Epic sadface"