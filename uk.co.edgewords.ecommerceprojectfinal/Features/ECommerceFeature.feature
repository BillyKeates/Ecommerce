Feature: E-Commerce website validation

Background:
	Given I have added an item to the cart

@TestCase1
Scenario: Applying a discount code
	When I apply the coupon 'edgewords' on the cart page
	Then the total cost reflects the discount of 15%
	

@TestCase2
Scenario: Order number shows in order history
	When I go through the checkout process with these credentials
	| userFName | userLName | userCountry    | userStreet   | userCity  | userPostcode | userPhoneNo  |
	| first     | last      | United Kingdom | 30 test road | test city | YO10 4DP     | 07777 777777 |
	Then my order will appear in my order history

