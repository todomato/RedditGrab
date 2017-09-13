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
                    //var a = node.Attributes["href"].Value;
                    //var a = node.Attributes["data-fullname"].Value; //文章編號
                    textBox2.Text += HttpUtility.HtmlDecode(node.InnerText.Trim()) + Environment.NewLine;
                    //textBox2.Text += node.Attributes["datetime"].Value;
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

        /// <summary>
        /// 匯出excel
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
                    var data = db.List.ToList();
                    var data2 = db.Reply.ToList();

                    ExportExcel.ExportData(data, saveFileDialog1.FileName, "列表匯出");
                    ExportExcel.ExportData(data2, saveFileDialog1.FileName + "_清單", "清單匯出");

                    MessageBox.Show("匯出完畢!");
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
    }
}
