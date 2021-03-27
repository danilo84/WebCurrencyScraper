# WebCurrencyScraper
c# console app-WebCurrencyScraper

Program  automates extraction of data from a webpage
https://srh.bankofchina.com/search/whpj/searchen.jspYou%20should%20see%20something%20like%20this:.
It navigates to the  website, fills in the form, posts back to the website with appropriate form parameters, 
“scrapes” the returned data, and outputs it to a text file (CSV format). The program loops through the contents
of a drop-down menu and do the scraping procedure for each available data set (currency).

Fils in the form:
 Start date should be the current date -2 days.
 End date should be the current date.

Before running application on local computer, create folder Reports on C partition. In this folder report files will be created.
