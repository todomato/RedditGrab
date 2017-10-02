using HtmlAgilityPack;
using HtmlParseForm.Helpers;
using HtmlParseForm.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace HtmlParseForm
{
    public partial class Form1 : Form
    {
        string title_path = "/html/body/div[4]/div[2]/div/div/div[{0}]/div/header/a";   //  標題
        string count_path = "/html/body/div[4]/div[2]/div/div/div[{0}]/div/div[1]/a";   //   留言數
        string id_path = "/html/body/div[4]/div[2]/div/div/div[{0}]";  //   ID
        string author_path = "/html/body/div[4]/div[2]/div/div/div[{0}]/div/div[1]/span[4]/a";  //   author
        string desc_path = "/html/body/div[4]/div[2]/div/div/div[{0}]/div/div[2]/div/div/p";    //   內容

        public Form1()
        {
            InitializeComponent();
            txt_Url.Text = @"https://www.reddit.com/r/taiwan/search?q=flair%3A%27travel%27&sort=new&restrict_sr=on&t=all";

        }

        /// <summary>
        /// 測試用查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_query_Click(object sender, EventArgs e)
        {
            
            textBox2.Text = "...";
            var url = "";
            string xpath = "/html/body/div[4]/div[2]/div/div/div[1]/div/header/a";

            textBox2.Text = "";
            url = (string.IsNullOrEmpty(txt_Url.Text) ? url : txt_Url.Text);
            xpath = (string.IsNullOrEmpty(txt_xPath.Text)) ? xpath : txt_xPath.Text;

            //下載html
            var doc = ParseHtmlToDoc(url, ck_unicode.Checked);

            //解析html
            try
            {
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);
                foreach (HtmlNode node in nodes)
                {
                    // 抓detail
                    //textBox2.Text += node.Attributes["href"].Value;
                    //var a = node.Attributes["data-fullname"].Value; //文章編號
                    //textBox2.Text += node.InnerHtml.Trim() + Environment.NewLine;
                    //textBox2.Text += node.InnerText.Trim() + Environment.NewLine;
                    textBox2.Text += node.Attributes["title"].Value;
                }

                // /html/body/div[4]/div[2]/div/div/div  計算每頁數字
                textBox2.Text += "   " + nodes.Count();
            }
            catch (Exception ex)
            {
                textBox2.Text = ex.Message;
            }
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            insertData();
        }

        /// <summary>
        /// 查詢列表
        /// </summary>
        /// <param name="path"></param>
        /// <param name="total_count"></param>
        private void insertData(string path = "", int total_count = 0)
        {
            textBox2.Text = "";

            //接關
            if (!string.IsNullOrEmpty(txt_lastID.Text))
            {
                var next_page_path = string.Format("https://www.reddit.com/r/taiwan/search?q=flair%3A%27events%27&sort=new&restrict_sr=on&t=all&count=25&after={0}", txt_lastID.Text);
                txt_lastID.Text = "";
                insertData(next_page_path);
            }
            else
            {
                var url = path == "" ? "https://www.reddit.com/r/taiwan/search?q=flair%3A%27events%27&sort=new&restrict_sr=on&t=all" : path;
                string xpath = "/html/body/div[4]/div[2]/div/div/div";  //列表

                //下載html
                var doc = ParseHtmlToDoc(url, ck_unicode.Checked);

                //解析html
                try
                {
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);
                    var count = nodes.Count;
                    var list = new List<List>();
                    for (int i = 1; i <= count; i++)
                    {
                        var temp = new HtmlParseForm.Models.List();
                        temp.ID = doc.DocumentNode.SelectNodes(string.Format(id_path, i))[0].Attributes["data-fullname"].Value;
                        temp.Title = doc.DocumentNode.SelectNodes(string.Format(title_path, i))[0].InnerText.Trim();
                        temp.Author = doc.DocumentNode.SelectNodes(string.Format(author_path, i))[0].InnerText.Trim();
                        temp.Url = doc.DocumentNode.SelectNodes(string.Format(title_path, i))[0].Attributes["href"].Value;
                        //temp.Description = doc.DocumentNode.SelectNodes(string.Format(desc_path, i))[0].InnerText.Trim();
                        temp.MessageCount = doc.DocumentNode.SelectNodes(string.Format(count_path, i))[0].InnerText.Trim();
                        temp.Type = "Events";
                        list.Add(temp);
                    }

                    // save data
                    using (var db = new RedditScanEntities())
                    {
                        db.List.AddRange(list);
                        db.SaveChanges();
                    }

                    // 讀取最後一筆id, 組成下一頁url
                    var next_page_path = string.Format("https://www.reddit.com/r/taiwan/search?q=flair%3A%27events%27&sort=new&restrict_sr=on&t=all&count=25&after={0}", list.Last().ID);

                    if (total_count < 40)
                    {
                        total_count++;
                        insertData(next_page_path, total_count);
                    }
                }
                catch (Exception ex)
                {
                    textBox2.Text += ex.Message + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 查詢內頁資訊1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            var xpath = @"//*[@id=""thing_{0}""]/div[2]/div[2]/div";
            var time_path = @"//*[@id=""thing_{0}""]/div[2]/div/p[2]/time";

            var list = new List<List>();
            using (var db = new RedditScanEntities())
            {
                list = db.List.Where(c => c.PostDate == null).ToList();

                foreach (var item in list)
                {
                    //下載html
                    var doc = ParseHtmlToDoc(item.Url, ck_unicode.Checked);

                    //解析html
                    try
                    {
                        // 內文
                        HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(string.Format(xpath, item.ID));
                        if (nodes != null)
                        {
                            foreach (HtmlNode node in nodes)
                            {
                                item.Description = node.InnerText.Trim();
                            }
                        }
                       

                        // 日期
                        HtmlNodeCollection nodes2 = doc.DocumentNode.SelectNodes(string.Format(time_path, item.ID));
                        if (nodes2 != null)
                        {
                            foreach (HtmlNode node in nodes2)
                            {
                                item.PostDate = DateTime.Parse(node.Attributes["datetime"].Value);
                            }
                        }
                     
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        textBox2.Text += ex.Message + Environment.NewLine;
                    }
                }
            }
        }

        /// <summary>
        /// 查詢內頁資訊2 : 回覆資訊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            var list = new List<List>();
            using (var db = new RedditScanEntities())
            {
                list = db.List.OrderBy(c => c.ID).ToList();

                foreach (var item in list)
                {
                    //下載html
                    var doc = ParseHtmlToDoc(item.Url, ck_unicode.Checked);

                    //解析html
                    try
                    {
                        // 回復文章
                        string classToFind = "entry unvoted";

                        //  //*[contains(@class,'commentarea')]//*[contains(@class,'entry unvoted')]/div
                        var nodes =
                           doc.DocumentNode.SelectNodes(string.Format("//*[contains(@class,'commentarea')]//*[contains(@class,'{0}')]/div", classToFind));
                        var count = nodes.Count / 2;
                        var replys = new List<Reply>();
                        foreach (var reply in nodes)
                        {
                            var temp = new HtmlParseForm.Models.Reply();
                            if (string.IsNullOrEmpty(reply.InnerText.Trim())) continue;
                            temp.Message = reply.InnerText.Trim();
                            temp.ID = item.ID;
                            replys.Add(temp);
                        }

                        // 作者
                        var nodes2 =
                        doc.DocumentNode.SelectNodes(string.Format("//*[contains(@class,'commentarea')]//*[contains(@class,'{0}')]/p/a[2]", classToFind));
                        var i = 0;
                        foreach (var author in nodes2)
                        {
                            if (string.IsNullOrEmpty(author.InnerText.Trim()) || count <= i) continue;
                            replys[i].Author = author.InnerText.Trim();
                            i++;
                        }

                        // 回復時間
                        var nodes3 =
                        doc.DocumentNode.SelectNodes(string.Format("//*[contains(@class,'commentarea')]//*[contains(@class,'{0}')]/p/time", classToFind));
                        i = 0;
                        foreach (var time in nodes3)
                        {
                            if (string.IsNullOrEmpty(time.InnerText.Trim()) || count <= i) continue;
                            replys[i].PostDate = DateTime.Parse(time.Attributes["datetime"].Value);
                            i++;
                        }

                        db.Reply.AddRange(replys);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        textBox2.Text += ex.Message + Environment.NewLine;
                    }
                }
            }
        }

    

        #region 私人方法

        /// <summary>
        /// 下載網址資訊
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ischeckCode"></param>
        /// <returns></returns>
        private static HtmlAgilityPack.HtmlDocument ParseHtmlToDoc(string url, bool ischeckCode)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            using (var myWebClient = new WebClient())
            {

                if (ischeckCode)
                {
                    myWebClient.Encoding = Encoding.UTF8;
                }
                string page = myWebClient.DownloadString(url);
                //string page = System.Text.Encoding.GetEncoding("Big5").GetString(response);
                doc.LoadHtml(page);
            }
            return doc;
        }
        #endregion

        /// <summary>
        /// formosa list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {

            var title_xpath = @"//*[@id=""main-outlet""]/div[1]/div[{0}]/a"; //  取標題名稱、url、
            //var count_xpath = @"//*[@id=""main-outlet""]/div[1]/div[{0}]/span[2]";//    po文數 -1 
            //var template = "http://tw.forumosa.com/c/travel/travel-in-taiwan/l/latest?no_subcategories=false&page={0}&_=1505369146433"; //80
            //var template = "http://tw.forumosa.com/c/recreation/sports/l/latest?no_subcategories=false&page={0}&_=1505376888777";   //79
            //var template = "http://tw.forumosa.com/c/recreation/cycling/l/latest?no_subcategories=false&page={0}&_=1505376888777";  
            //var template = "http://tw.forumosa.com/c/recreation/martial-arts/l/latest?no_subcategories=false&page={0}&_=1505376888777";
            
            //var template = "http://tw.forumosa.com/c/humanities/culture-history/l/latest?no_subcategories=false&page={0}&_=1505377715473";  //42
            //var template = "http://tw.forumosa.com/c/humanities/religion-spirituality/l/latest?no_subcategories=false&page={0}&_=1505377843724"; //13
            //var template = "http://tw.forumosa.com/c/life/food-drink/l/latest?no_subcategories=false&page={0}&_=1505377938348";  //66
            //var template = "http://tw.forumosa.com/c/taiwan/where-can-i-find/l/latest?no_subcategories=false&page={0}&_=1505378114858"; //185

            var count_xpath = @"//*[@id=""main-outlet""]/div[1]/div[{0}]/span[3]";//    po文數 -1 
            //var template = "http://tw.forumosa.com/c/taiwan-events-listings/l/latest?no_subcategories=false&page={0}&_=1505377521774"; //85
            var template = "http://tw.forumosa.com/c/food/l/latest?no_subcategories=false&page={0}&_=1505378280543";
            //var template = "";

            for (int x = 0; x <= 185; x++)
			{
                var url = string.Format(template, x);
                string xpath = @"//*[@id=""main-outlet""]/div[1]/div/a";  //列表

                //下載html
                var doc = ParseHtmlToDoc(url, ck_unicode.Checked);

                //解析html
                try
                {
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);
                    var count = nodes.Count;
                    var list = new List<FormosaList>();
                    for (int i = 1; i <= count; i++)
                    {
                        var temp = new HtmlParseForm.Models.FormosaList();
                        temp.Url = "http://tw.forumosa.com" + 
                            doc.DocumentNode.SelectNodes(string.Format(title_xpath, i))[0].Attributes["href"].Value;
                        temp.Title = HttpUtility.HtmlDecode(doc.DocumentNode.SelectNodes(string.Format(title_xpath, i))[0].InnerText.Trim());
                        temp.RepliesCount = int.Parse(doc.DocumentNode.SelectNodes(string.Format(count_xpath, i))[0].InnerText.Trim().Replace("(", "").Replace(")", ""));

                        //Travel
                        //Recreation_sports
                        //Recreation_cycle
                        //Recreation_art
                        //Humanities_Culture
                        //Humanities_Religion
                        //Life_food
                        //Taiwan_Where
                        //Events
                        //Restaurants
                        temp.Type = "";
                        list.Add(temp);
                    }

                    // save data
                    using (var db = new RedditScanEntities())
                    {
                        db.FormosaList.AddRange(list);
                        db.SaveChanges();
                    }

                    if (count != 30)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    textBox2.Text += ex.Message + Environment.NewLine;
                }
			}

            MessageBox.Show("爬完了");
        }

        /// <summary>
        /// formosa 內頁資訊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            var author_xpath = "//*[contains(@class,'creator')]/span[1]/a";  //作者
            var postdate_xpath = "//*[contains(@class,'creator')]/span[1]/time";  //時間
            var desc_xpath = "//div[contains(@class,'post')]"; // 內文

             using (var db = new RedditScanEntities())
             {
                var  list = db.FormosaList.OrderBy(c => c.CreateTime).ToList();

                 foreach (var item in list)
                 {
                     //下載html
                     var doc = ParseHtmlToDoc(item.Url, ck_unicode.Checked);

                     //解析html
                     try
                     {
                         item.Author= doc.DocumentNode.SelectNodes(author_xpath)[0].InnerText.Trim();
                         item.PostDate = DateTime.Parse(doc.DocumentNode.SelectNodes(postdate_xpath)[0].Attributes["datetime"].Value);
                         //HttpUtility.HtmlDecode(node.InnerText.Trim())
                         item.Description = HttpUtility.HtmlDecode(doc.DocumentNode.SelectNodes(desc_xpath)[0].InnerText.Trim());
                         db.SaveChanges();

                     }
                     catch (Exception ex)
                     {
                         textBox2.Text += ex.Message + Environment.NewLine;
                     }
                 }
             }
        }

        /// <summary>
        /// formosa 回復
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            //TODO
            // 注意是回復一次只有20筆 http://tw.forumosa.com/t/st-patricks-day-party-at-the-taipei-artist-village/57000/16
            // 藥用後面tag來產生後續的回覆內容, 注意一定會有重複筆數

            var author_xpath = "//*[contains(@class,'creator')]/span[1]/a";  //作者
            var postdate_xpath = "//*[contains(@class,'creator')]/span[1]/time";  //時間
            var desc_xpath = "//div[contains(@class,'post')]"; // 內文
            var id_xpath = "//div[contains(@class,'creator')]/span[2]"; // id

            using (var db = new RedditScanEntities())
            {
                var time = DateTime.Parse("2017-09-14 17:57:49.257");
                var list = db.FormosaList
                    .Where(c => c.CreateTime >= time)
                    .OrderBy(c => c.CreateTime).ToList();

                foreach (var item in list)
                {
                    // 留言數每超過20筆,需要另外存取

                    for (int i = 0; i < item.RepliesCount; i = i + 20)
                    {
                        //下載html
                        var path = string.Format("{0}/{1}", item.Url, i + 6);   //頁面會預先往前讀取5筆資料
                        var doc = ParseHtmlToDoc(path, ck_unicode.Checked);

                        //解析html
                        try
                        {
                            var replys = new List<FormosaReply>();
                            var nodes = doc.DocumentNode.SelectNodes(author_xpath);

                            // 作者
                            foreach (var author in nodes)
                            {
                                var temp = new HtmlParseForm.Models.FormosaReply();
                                temp.Author = author.InnerText.Trim();
                                temp.UID_List = item.UID;
                                replys.Add(temp);
                            }

                            // ID
                            var nodes2 = doc.DocumentNode.SelectNodes(id_xpath);
                            var x = 0;
                            foreach (var id in nodes2)
                            {
                                replys[x].ID = id.InnerText.Trim();
                                x++;
                            }

                            // postdate
                            var nodes3 = doc.DocumentNode.SelectNodes(postdate_xpath);
                            x = 0;
                            foreach (var postdate in nodes3)
                            {
                                replys[x].PostDate = DateTime.Parse(postdate.Attributes["datetime"].Value);
                                x++;
                            }
                            // message 
                            x = 0;
                            var nodes4 = doc.DocumentNode.SelectNodes(desc_xpath);
                            foreach (var message in nodes4)
                            {
                                replys[x].Message = HttpUtility.HtmlDecode(message.InnerText.Trim());
                                x++;
                            }

                            db.FormosaReply.AddRange(replys);
                            db.SaveChanges();

                        }
                        catch (Exception ex)
                        {
                            textBox2.Text += ex.Message + Environment.NewLine;
                        }
                    }
                  
                }
            }
        }

        /// <summary>
        /// 匯出reddit excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {

            saveFileDialog1.ShowDialog(this.ParentForm);
            if (saveFileDialog1.FileName.Length > 0)
            {
                //匯出
                using (var db = new RedditScanEntities())
                {
                    //var data = db.List.ToList();
                    //var data2 = db.Reply.ToList();

                    //var data3 = db.RedditAll.OrderByDescending(c => c.CreateTime).ThenBy(c => c.ReplyPostDate).ToList();
                    var data4 = db.Database.SqlQuery<RedditAllViewModel>(@"
                          select * from [RedditScan].[dbo].[RedditAll] ORDER BY [CreateTime] DESC, [ReplyPostDate] ASC
                    ").ToList();

                    //ExportExcel.ExportData(data, saveFileDialog1.FileName, "列表匯出");
                    //ExportExcel.ExportData(data2, saveFileDialog1.FileName + "_清單", "清單匯出");
                    ExportExcel.ExportData(data4, saveFileDialog1.FileName, "清單匯出");

                    MessageBox.Show("匯出完畢!");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog(this.ParentForm);
            if (saveFileDialog1.FileName.Length > 0)
            {
                //匯出
                using (var db = new RedditScanEntities())
                {
                    //var data = db.FormosaList.AsNoTracking().OrderBy(c => c.CreateTime).ToList();
                    var data2 = db.FormosaReply2.AsNoTracking().OrderBy(c => c.UID_List).ThenBy(c => c.ID).ToList();

                    ExportExcel.ExportData(data2, saveFileDialog1.FileName, "清單匯出");

                    MessageBox.Show("匯出完畢!");
                }
            }
        }

        //攜程列表
        private void button13_Click(object sender, EventArgs e)
        {
            var url_xpath = @"/html/body/div[4]/div/div[2]/div/div[2]/a"; //  url
            var title_xpath = @"/html/body/div[4]/div/div[2]/div/div[2]/a/div/dl/dt";  //  取標題名稱
            var date_xpath = @"/html/body/div[4]/div/div[2]/div/div[2]/a/div/dl/dd";      //日期    
            var author_xpath = @"/html/body/div[4]/div/div[2]/div/div[2]/a/div/dl/dd";    //作者    
            var likes_xpath = @"/html/body/div[4]/div/div[2]/div/div[2]/a/div/ul/li[2]/i";    //喜歡    
            var replies_xpath = @"/html/body/div[4]/div/div[2]/div/div[2]/a/div/ul/li[3]/i";    //回復次數  
          
            var template = "http://you.ctrip.com/travels/taiwan100076/t3-p{0}.html";

            for (int x = 1; x <= 557; x++)
            {
                textBox2.Text += "更新列表 : " + x + Environment.NewLine;
                Application.DoEvents();

                var url = string.Format(template, x);

                //下載html
                var doc = ParseHtmlToDoc(url, ck_unicode.Checked);
                //解析html
                try
                {
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(url_xpath);
                    var count = nodes.Count;
                    var list = new List<ChenList>();
                    for (int i = 0; i < count; i++)
                    {
                        var temp =  new HtmlParseForm.Models.ChenList();
                        temp.Url = "http://you.ctrip.com/" + doc.DocumentNode.SelectNodes(string.Format(url_xpath, i))[i].Attributes["href"].Value;
                        temp.Title = HttpUtility.HtmlDecode(doc.DocumentNode.SelectNodes(string.Format(title_xpath, i))[i].InnerText.Trim());
                        var  author_raw =  HttpUtility.HtmlDecode(doc.DocumentNode.SelectNodes(string.Format(author_xpath, i))[i*3].InnerText.Trim());
                        temp.Author =  HttpUtility.HtmlDecode(author_raw.Substring(0, author_raw.IndexOf("发表于")));
                        var aaa = author_raw.Substring(author_raw.IndexOf("发表于")+4, 10);
                        temp.PostDate = DateTime.Parse(author_raw.Substring(author_raw.IndexOf("发表于") + 4, 10));
                        temp.RepliesCount = int.Parse(doc.DocumentNode.SelectNodes(string.Format(replies_xpath, i))[i].InnerText.Trim());
                        temp.Likes = int.Parse(doc.DocumentNode.SelectNodes(string.Format(likes_xpath, i))[i].InnerText.Trim());
                        temp.Type = "";
                        list.Add(temp);
                    }

                    // save data
                    using (var db = new RedditScanEntities())
                    {
                        db.ChenList.AddRange(list);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    textBox2.Text += ex.Message + Environment.NewLine;
                    Application.DoEvents();
                }
            }

            MessageBox.Show("爬完了");
        }


        /// <summary>
        /// chen 內頁資訊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            var desc_xpath = @"//*[@class=""ctd_content wtd_content""]"; // 內文

            using (var db = new RedditScanEntities())
            {
                var list = db.ChenList.Where(c => c.Description == null).OrderBy(c => c.CreateTime).ToList();

                foreach (var item in list)
                {
                    textBox2.Text += item.Title + Environment.NewLine;
                    Application.DoEvents();

                    //下載html
                    var doc = ParseHtmlToDoc(item.Url, ck_unicode.Checked);

                    //解析html
                    try
                    {
                        item.Description = HttpUtility.HtmlDecode(doc.DocumentNode.SelectNodes(desc_xpath)[0].InnerText.Trim());
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        textBox2.Text += ex.Message + Environment.NewLine;
                    }
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var desc_xpath = @"//*[@id=""authorDisplayName""]"; // 內文

            using (var db = new RedditScanEntities())
            {
                var list = db.ChenList.Where(c => c.AuthorUrl == null).OrderBy(c => c.CreateTime).ToList();

                foreach (var item in list)
                {
                    textBox2.Text += item.Title + Environment.NewLine;
                    Application.DoEvents();

                    //下載html
                    var doc = ParseHtmlToDoc(item.Url, ck_unicode.Checked);

                    //解析html
                    try
                    {
                        item.AuthorUrl = "http://you.ctrip.com/" + doc.DocumentNode.SelectNodes(desc_xpath)[0].Attributes["href"].Value;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        textBox2.Text += ex.Message + Environment.NewLine;
                    }
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var name_xapth = @"/html/body/div[2]/div/div[2]/div[2]/div[1]/span[1]";
            var address_xpath = @"/html/body/div[2]/div/div[2]/div[2]/div[2]/span[2]";
            var male_xpath = @"/html/body/div[2]/div/div[2]/div[2]/div[2]/span[1]/i"; //title
            var follows_xpath = @"/html/body/div[2]/div/div[2]/div[3]/ul/li[2]/strong/a";
            // TODO 遊記數量以爬到數量為主

            using (var db = new RedditScanEntities())
            {
                var list = db.ChenProfile.Where(c => c.Address == null).OrderBy(c => c.ID).ToList();

                foreach (var item in list)
                {
                    textBox2.Text += item.ID + Environment.NewLine;
                    Application.DoEvents();

                    //下載html
                    var doc = ParseHtmlToDoc(item.ID, ck_unicode.Checked);

                    //解析html
                    try
                    {
                        item.Name = HttpUtility.HtmlDecode(doc.DocumentNode.SelectNodes(name_xapth)[0].InnerText.Trim());
                        item.Address = HttpUtility.HtmlDecode(doc.DocumentNode.SelectNodes(address_xpath)[0].InnerText.Trim());
                        item.Address = item.Address.Contains("现居") ? item.Address : null;
                        item.Sex = HttpUtility.HtmlDecode(doc.DocumentNode.SelectNodes(male_xpath)[0].Attributes["title"].Value.Trim());
                        item.Follows = int.Parse(doc.DocumentNode.SelectNodes(follows_xpath)[0].InnerText.Trim());
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        textBox2.Text += ex.Message + Environment.NewLine;
                    }
                }
            }
        }
    }
}
