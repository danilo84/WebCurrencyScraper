using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using System.IO;

namespace WwwebScraper
{
    class Program
    {
        static void Main(string[] args)


        {

            IWebDriver driver = new ChromeDriver();



            driver.Url = "https://srh.bankofchina.com/search/whpj/searchen.jsp";

            // Maximizacija Window-a

            driver.Manage().Window.Maximize();

            // podesavanje datuuma po sistemu
            var vreme = DateTime.Today;
            var sistemskidatum = driver.FindElement(By.CssSelector("#historysearchform > table > tbody > tr > td:nth-child(4) > input[type=text]"));
            sistemskidatum.SendKeys(vreme.ToString("yyyy-MM-dd"));
            //vreme potrebno za naziv fajla sa podacima
            var sistemskinaziv2 = vreme.ToString("yyyy-MM-dd");

            // podesavanje datuuma minus dva dana
            var prvovreme = vreme.Date.AddDays(-2);
            var prvidatum = driver.FindElement(By.CssSelector("#historysearchform > table > tbody > tr > td.hui12_20_hover > input[type=text]"));
            prvidatum.SendKeys(prvovreme.ToString("yyyy-MM-dd"));
            //prvovreme potrebno za naziv fajla sa podacima
            var sistemskinaziv1 = prvovreme.ToString("yyyy-MM-dd");

            // Podesavanje Dropdownliste
            //prolazak kroz prvu stavku u ddlisti

            var izaberivalutu = driver.FindElement(By.CssSelector("#pjname"));
            izaberivalutu.SendKeys(Keys.Down + Keys.Enter + Keys.Enter);
            //vrednost potrebno za naziv fajla sa podacima
            var izaberivalutunaziv1 = izaberivalutu.ToString();
            var ddvaluta1 = driver.FindElement(By.XPath("//html/body/table[1]/tbody/tr/td[4]/select")).GetAttribute("value");

            //kreiranje prvog fajla na C

            string putanja1 = @"C:\\Reports\\Report" + sistemskinaziv1 + "-" + sistemskinaziv2 + "-" + ddvaluta1 + ".csv";
            string csvpath1 = putanja1.ToString();

            //kreiranje fajla
            StringBuilder csvdokument1 = new StringBuilder();
            csvdokument1.AppendLine();

            //upis

            var node1 = new WebDriverWait(driver, new TimeSpan(0, 0, 0, 3, 0)).Until(OpenQA.Selenium.Support.UI.ExpectedConditions.PresenceOfAllElementsLocatedBy(
                    By.XPath(".//html/body/table[1]/tbody/tr/td[5]/input"))).First();


            node1.Click();



            node1.ToString();

            string nestoPr = driver.FindElement(By.CssSelector("body > table:nth-child(4)")).Text;

            Console.WriteLine(nestoPr);


            List<IWebElement> objList1 = driver.FindElements(By.TagName("a")).ToList();
            int m = objList1.Count();

            Console.WriteLine(m);
            //dodaje se prva strana prve valute
            csvdokument1.AppendLine(nestoPr.ToString());


            //ovde ide m-1 u petlji zato sto poslednju stranu ne stavljaju sa tagom "a" vec kao neki span verovatno dok ne popune tu zadnju stranu, a posto se vec nalazi na prvoj strani da bi isao do poslednje trebalo bi m-1

            for (var j = 0; j <= (m - 1); j++)
            {
                //provera da li je ista nasao da li da kline nextab ili dalje dropdown listu da izabere sledece
                bool CheckIfItExists1(string ElementXpath)
                {
                    try
                    {
                        driver.FindElement(By.XPath(".//html/body/table[3]/tbody/tr/td/div/span[3]/a"));
                        return true;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                }

                if (CheckIfItExists1(".//html/body/table[3]/tbody/tr/td/div/span[3]/a"))
                {


                    var nextTab1 = driver.FindElement(By.XPath(".//html/body/table[3]/tbody/tr/td/div/span[3]/a"));

                    nextTab1.Click();

                }
                else
                {
                    var nastavi1 = driver.FindElement(By.CssSelector("#pjname"));
                    nastavi1.SendKeys(Keys.Down + Keys.Tab + Keys.Enter);
                }

                string nesto1 = driver.FindElement(By.CssSelector("body > table:nth-child(4)")).Text;

                Console.WriteLine(nesto1);
                Console.WriteLine(m);

                //dodaju se ostale strane prve valute
                csvdokument1.AppendLine(nesto1.ToString());

            }

            File.AppendAllText(csvpath1, csvdokument1.ToString());

            var selectElement = new SelectElement(driver.FindElement(By.Id("pjname")));
            var optionCount = selectElement.Options.Count;

            //----------------------------------------------------------------------------------------

            // loop za sve valute
            var provera = driver.FindElement((By.XPath(".//html/body/table[2]/tbody/tr/td")));
            //ne racunam prvu valutu u ddl koju je vec prosao i ne racunam select the currency zato je minus 2
            for (int i = 0; i < optionCount - 2; i++)
            {

                var izabeerivalutu = driver.FindElement(By.CssSelector("#pjname"));
                izabeerivalutu.SendKeys(Keys.Down + Keys.Tab + Keys.Enter);

                var izaberivalutunaziv = izabeerivalutu.ToString();

                var ddvaluta = driver.FindElement(By.XPath("//html/body/table[1]/tbody/tr/td[4]/select")).GetAttribute("value");
                //kreiranje fajla na C

                string putanja = @"C:\\Reports\\Report" + sistemskinaziv1 + "-" + sistemskinaziv2 + "-" + ddvaluta + ".csv";
                string csvpath = putanja.ToString();

                //kreiranje fajla
                StringBuilder csvdokument = new StringBuilder();
                csvdokument.AppendLine();

                //upis ako je nesto nasao 

                bool CheckIfItExists2(string ElementXpath)
                {
                    try
                    {
                        driver.FindElement(By.XPath(".//html/body/table[3]/tbody/tr/td/div/span[3]/a"));
                        return true;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                }



                if (CheckIfItExists2(".//html/body/table[3]/tbody/tr/td/div/span[3]/a"))
                {

                    var node11 = new WebDriverWait(driver, new TimeSpan(0, 0, 0, 3, 0)).Until(OpenQA.Selenium.Support.UI.ExpectedConditions.PresenceOfAllElementsLocatedBy(
                    By.XPath(".//html/body/table[1]/tbody/tr/td[5]/input"))).First();
                    node11.Click();
                    node11.ToString();

                    string nestoPr1 = driver.FindElement(By.CssSelector("body > table:nth-child(4)")).Text;

                    Console.WriteLine(nestoPr1);
                    //dodaje se prva strana prve valute
                    csvdokument.AppendLine(nestoPr1.ToString());

                    //-------------------------------------------------------------------- 

                    for (var j = 0; j <= (m - 1); j++)
                    {
                        //provera da li je ista nasao da li da kline nettab ili dalje dropdown listu da izabere sledece
                        bool CheckIfItExists13(string ElementXpath)
                        {
                            try
                            {
                                driver.FindElement(By.XPath(".//html/body/table[3]/tbody/tr/td/div/span[3]/a"));
                                return true;
                            }
                            catch (NoSuchElementException)
                            {
                                return false;
                            }
                        }

                        if (CheckIfItExists13(".//html/body/table[3]/tbody/tr/td/div/span[3]/a"))
                        {


                            var nextTab13 = driver.FindElement(By.XPath(".//html/body/table[3]/tbody/tr/td/div/span[3]/a"));

                            nextTab13.Click();

                        }
                        else
                        {
                            var nastavi13 = driver.FindElement(By.CssSelector("#pjname"));
                            nastavi13.SendKeys(Keys.Down + Keys.Tab + Keys.Enter);
                        }

                        string nesto13 = driver.FindElement(By.CssSelector("body > table:nth-child(4)")).Text;

                        Console.WriteLine(nesto13);
                        Console.WriteLine(m);

                        //dodaju se ostale strane prve valute
                        csvdokument.AppendLine(nesto13.ToString());
                    }


                    File.AppendAllText(csvpath, csvdokument.ToString());








                }
                
            }

            
        }

    }
}