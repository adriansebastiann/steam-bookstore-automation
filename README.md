# 📚 Steam Bookstore Automation

Automated test framework for the **Steam Bookstore** demo project.  
Built with **C# .NET 8**, **Reqnroll (BDD)**, **Selenium WebDriver**, **NUnit**, and **Allure Reports**.

## 📦 Tech Stack
- [.NET 8](https://dotnet.microsoft.com/)  
- [Reqnroll](https://reqnroll.net/) (BDD framework, SpecFlow-compatible)  
- [NUnit](https://nunit.org/)  
- [Selenium WebDriver](https://www.selenium.dev/)  
- [Allure Reports](https://docs.qameta.io/allure/)  

## ⚙️ Setup

1. **Clone repository**
   ```bash
   git clone https://github.com/your-username/steam-bookstore-automation.git
   cd steam-bookstore-automation

2. **Restore dependencies**
dotnet restore

3. **Build solution**
dotnet build


## Running Tests
dotnet test
dotnet test --logger:"html;LogFileName=test-report.html"

## Allure Reports
dotnet test
allure serve allure-results

## Project Structure
Features/        -> BDD feature files (.feature)
Steps/           -> Step definitions (Given/When/Then)
Pages/           -> Page Object Models (UI locators & actions)
Drivers/         -> WebDriver factory 
Hooks/           -> Test lifecycle hooks (setup, teardown, Allure integration, screenshots)
Config/          -> Configuration files
allure-results/  -> Raw test results for Allure (ignored in git)
allure-report/   -> Generated HTML report (ignored in git)

