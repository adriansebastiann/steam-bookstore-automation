Feature: Replace a book via API
  As an API client
  I want to create a user and manage their books
  So that I can replace an existing book with another

  @api @bookstore
  Scenario: Replace the user's book with the second book from the list
    Given a new authorized user
    When I get the list of all books
    And I add the first book to the user's collection
    And I get the user by id
    Then the user has exactly 1 book and it matches the added book
    When I replace the user's book with the second book
    And I get the user by id
    Then the user has exactly 1 book and it is the second book
    And I delete the created user
