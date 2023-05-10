Feature: SL_Buying

As a registered user of the Sauce Labs website
I want to be able to add items to by basket
and then checkout

Background: 
    Given I am logged in

@tag1
Scenario: Adding single item to basket
    When I click to add the sauce labs backpack to cart
    Then the basket counter should increase by 1