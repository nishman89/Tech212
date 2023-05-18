Feature: SinglePostcodeRequest

In order to get the details of a postcode
As a user
I want to be able to make a request for a single postcode on postcodes.io

Background: 
Given I have initialised the Single Postcode Service

@Happy
Scenario: Request for a valid postcode returns valid status code in response header and JSON response body
	When I make a request for the postcode "EC2Y 5AS"
	Then the status in the jSON response body should be 200
	And the status header should be 200

@Happy
Scenario: Request for a valid postcode returns a JSON response body with the correct schema
	When I make a request for the postcode "EC2Y 5AS"
	Then the JSON response body match the Json schema in "SuccessfulSinglePostcodeResponse.json"

@Happy
Scenario: Request for a valid postcode I expect all the headers to be correct
    When I make a request for the postcode "EC2Y 5AS"
    Then the response headers should contain the following headers:
        | key               | value      |
        | Transfer-Encoding | chunked    |
        | Connection        | keep-alive |
        | Server            | cloudflare |

