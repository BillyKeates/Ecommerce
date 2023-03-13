Feature: E-Commerce website validation

Background:
	Given I have added an item to the cart

@TestCase1
Scenario: Applying a discount code
	When I apply the coupon 'edgewords' on the cart page
	Then the correct total cost is calculated
	

@TestCase2
Scenario: Recieving an order number
	When I go through the checkout process
	Then my order will appear in my order history

