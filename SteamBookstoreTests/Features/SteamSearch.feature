Feature: Steam search and navigate to About page
  As a user
  I want to search for FIFA and navigate to About Steam
  So that I can verify download and stats

  @ui @steam
  Scenario: Search 'FIFA' and navigate using first result via JavaScript click
    Given I open Steam in incognito
    When I search for "FIFA"
    Then the first two suggestions are "EA SPORTS FC™ 25" and "FIFA 22"
    When I click the first suggestion using JavaScript
    Then the game page is displayed and the title matches the first suggestion
    When I click the Download button in the header
    And I click "No, I need Steam"
    Then the About Steam page is displayed
    And the Install Steam button is clickable
    And the number of Playing Now gamers is less than the number of Online gamers
