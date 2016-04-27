using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class DBMilestone1 : Form
    {
        public static Dictionary<string, string> stateToAbbrev = new Dictionary<string, string>() { { "alabama", "AL" }, { "alaska", "AK" }, { "arizona", "AZ" }, { "arkansas", "AR" }, { "california", "CA" }, { "colorado", "CO" }, { "connecticut", "CT" }, { "delaware", "DE" }, { "district of columbia", "DC" }, { "florida", "FL" }, { "georgia", "GA" }, { "hawaii", "HI" }, { "idaho", "ID" }, { "illinois", "IL" }, { "indiana", "IN" }, { "iowa", "IA" }, { "kansas", "KS" }, { "kentucky", "KY" }, { "louisiana", "LA" }, { "maine", "ME" }, { "maryland", "MD" }, { "massachusetts", "MA" }, { "michigan", "MI" }, { "minnesota", "MN" }, { "mississippi", "MS" }, { "missouri", "MO" }, { "montana", "MT" }, { "nebraska", "NE" }, { "nevada", "NV" }, { "new hampshire", "NH" }, { "new jersey", "NJ" }, { "new mexico", "NM" }, { "new york", "NY" }, { "north carolina", "NC" }, { "north dakota", "ND" }, { "ohio", "OH" }, { "oklahoma", "OK" }, { "oregon", "OR" }, { "pennsylvania", "PA" }, { "rhode island", "RI" }, { "south carolina", "SC" }, { "south dakota", "SD" }, { "tennessee", "TN" }, { "texas", "TX" }, { "utah", "UT" }, { "vermont", "VT" }, { "virginia", "VA" }, { "washington", "WA" }, { "west virginia", "WV" }, { "wisconsin", "WI" }, { "wyoming", "WY" } };
        public static Dictionary<int, string> catagoryMonicker = new Dictionary<int, string>() { { 1, "d" }, { 2, "e" }, { 3, "f" }, { 4, "g" }, { 5, "h" }, { 6, "i" } };
        MySQL_Connection myDB;
        public DBMilestone1()
        {
            InitializeComponent();
            AgeDistributionZIP.Rows[0].Cells[0].Value = "Under 18 years";
            AgeDistributionZIP.Rows.Add("18 to 24 years", "");
            AgeDistributionZIP.Rows.Add("25 to 44 years", "");
            AgeDistributionZIP.Rows.Add("45 to 64 years", "");
            AgeDistributionZIP.Rows.Add("65 and over", "");
            AgeDistributionCITY.Rows[0].Cells[0].Value = "Under 18 years";
            AgeDistributionCITY.Rows.Add("18 to 24 years", "");
            AgeDistributionCITY.Rows.Add("25 to 44 years", "");
            AgeDistributionCITY.Rows.Add("45 to 64 years", "");
            AgeDistributionCITY.Rows.Add("65 and over", "");
            AgeDistributionSTATE.Rows[0].Cells[0].Value = "Under 18 years";
            AgeDistributionSTATE.Rows.Add("18 to 24 years", "");
            AgeDistributionSTATE.Rows.Add("25 to 44 years", "");
            AgeDistributionSTATE.Rows.Add("45 to 64 years", "");
            AgeDistributionSTATE.Rows.Add("65 and over", "");
            for (int i=0; i<6; i++)
            {
                comboBox4.Items.Add(i);
                comboBox5.Items.Add(i);
            }

            

            //initialize SQL
            myDB = new MySQL_Connection();
            string getStates = "SELECT DISTINCT state FROM Demographics ORDER BY state; "; //db query to get states

            //once the proper query is used above, these lists should be populated
            List<String> states = myDB.SQLSELECTExec(getStates, "state");
            
            //use them to fill out the boxes on the winforms, combobox1, listbox1, and listbox2
            foreach (String state in states)
            {
                comboBox1.Items.Add(state);
                comboBox2.Items.Add(state);
            }
            string getCategories = "SELECT DISTINCT cname FROM catagories";
            List<String> categories = myDB.SQLSELECTExec(getCategories, "cname");
            foreach (String category in categories)
            {
                listBox3.Items.Add(category);
                listBox8.Items.Add(category);
            }
            
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            string getZipcodes = "SELECT zipcode FROM Demographics WHERE city='" + listBox1.SelectedItem.ToString() + "' AND state='" + comboBox1.SelectedItem.ToString() + "' ORDER BY zipcode"; //db query to get zipcodes
            List<String> zipcodes = myDB.SQLSELECTExec(getZipcodes, "zipcode");
            foreach (String zipcode in zipcodes)
            {
                listBox2.Items.Add(zipcode);
            }
            string getDemographics = "SELECT SUM(population) FROM Demographics WHERE city='" + listBox1.SelectedItem.ToString() + "' GROUP BY city;";
            textBox1.Text = myDB.SQLSELECTExec(getDemographics, "SUM(population)")[0].ToString();
            getDemographics = "SELECT AVG(avg_income) FROM Demographics WHERE city='" + listBox1.SelectedItem.ToString() + "' GROUP BY city;";
            textBox2.Text = myDB.SQLSELECTExec(getDemographics, "AVG(avg_income)")[0].ToString();
            getDemographics = "SELECT AVG(Median_age) FROM Demographics WHERE city='" + listBox1.SelectedItem.ToString() + "' GROUP BY city;";
            textBox9.Text = myDB.SQLSELECTExec(getDemographics, "AVG(Median_age)")[0].ToString();
            getDemographics = "SELECT under18years FROM Demographics WHERE city='" + listBox1.SelectedItem.ToString() + "';";
            AgeDistributionCITY.Rows[0].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "under18years")[0].ToString();
            getDemographics = "SELECT 18_to_24years FROM Demographics WHERE city='" + listBox1.SelectedItem.ToString() + "';";
            AgeDistributionCITY.Rows[1].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "18_to_24years")[0].ToString();
            getDemographics = "SELECT 25_to_44years FROM Demographics WHERE city='" + listBox1.SelectedItem.ToString() + "';";
            AgeDistributionCITY.Rows[2].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "25_to_44years")[0].ToString();
            getDemographics = "SELECT 45_to_64years FROM Demographics WHERE city='" + listBox1.SelectedItem.ToString() + "';";
            AgeDistributionCITY.Rows[3].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "45_to_64years")[0].ToString();
            getDemographics = "SELECT 65_and_over FROM Demographics WHERE city='" + listBox1.SelectedItem.ToString() + "';";
            AgeDistributionCITY.Rows[4].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "65_and_over")[0].ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string getCities = "SELECT city FROM Demographics WHERE state='"+comboBox1.SelectedItem.ToString()+"' ORDER BY city;"; //db query to get cities
            List<String> cities = myDB.SQLSELECTExec(getCities, "city");
            foreach (String city in cities)
            {
                listBox1.Items.Add(city);
            }
            string getDemographics = "SELECT SUM(population) FROM Demographics WHERE state='" + comboBox1.SelectedItem.ToString() + "' GROUP BY state;";
            textBox6.Text = myDB.SQLSELECTExec(getDemographics, "SUM(population)")[0].ToString();
            getDemographics = "SELECT AVG(avg_income) FROM Demographics WHERE state='" + comboBox1.SelectedItem.ToString() + "' GROUP BY state;";
            textBox7.Text = myDB.SQLSELECTExec(getDemographics, "AVG(avg_income)")[0].ToString();
            getDemographics = "SELECT AVG(Median_age) FROM Demographics WHERE state='" + comboBox1.SelectedItem.ToString() + "' GROUP BY state;";
            textBox8.Text = myDB.SQLSELECTExec(getDemographics, "AVG(Median_age)")[0].ToString();
            getDemographics = "SELECT under18years FROM Demographics WHERE state='" + comboBox1.SelectedItem.ToString() + "';";
            AgeDistributionSTATE.Rows[0].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "under18years")[0].ToString();
            getDemographics = "SELECT 18_to_24years FROM Demographics WHERE state='" + comboBox1.SelectedItem.ToString() + "';";
            AgeDistributionSTATE.Rows[1].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "18_to_24years")[0].ToString();
            getDemographics = "SELECT 25_to_44years FROM Demographics WHERE state='" + comboBox1.SelectedItem.ToString() + "';";
            AgeDistributionSTATE.Rows[2].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "25_to_44years")[0].ToString();
            getDemographics = "SELECT 45_to_64years FROM Demographics WHERE state='" + comboBox1.SelectedItem.ToString() + "';";
            AgeDistributionSTATE.Rows[3].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "45_to_64years")[0].ToString();
            getDemographics = "SELECT 65_and_over FROM Demographics WHERE state='" + comboBox1.SelectedItem.ToString() + "';";
            AgeDistributionSTATE.Rows[4].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "65_and_over")[0].ToString();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string getDemographics = "SELECT population FROM Demographics WHERE zipcode='" + listBox2.SelectedItem.ToString() + "';";
            textBox3.Text = myDB.SQLSELECTExec(getDemographics, "population")[0].ToString();
            getDemographics = "SELECT avg_income FROM Demographics WHERE zipcode='" + listBox2.SelectedItem.ToString() + "';";
            textBox4.Text = myDB.SQLSELECTExec(getDemographics, "avg_income")[0].ToString();
            getDemographics = "SELECT Median_age FROM Demographics WHERE zipcode='" + listBox2.SelectedItem.ToString() + "';";
            textBox5.Text = myDB.SQLSELECTExec(getDemographics, "Median_age")[0].ToString();
            getDemographics = "SELECT under18years FROM Demographics WHERE zipcode='" + listBox2.SelectedItem.ToString() + "';";
            AgeDistributionZIP.Rows[0].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "under18years")[0].ToString();
            getDemographics = "SELECT 18_to_24years FROM Demographics WHERE zipcode='" + listBox2.SelectedItem.ToString() + "';";
            AgeDistributionZIP.Rows[1].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "18_to_24years")[0].ToString();
            getDemographics = "SELECT 25_to_44years FROM Demographics WHERE zipcode='" + listBox2.SelectedItem.ToString() + "';";
            AgeDistributionZIP.Rows[2].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "25_to_44years")[0].ToString();
            getDemographics = "SELECT 45_to_64years FROM Demographics WHERE zipcode='" + listBox2.SelectedItem.ToString() + "';";
            AgeDistributionZIP.Rows[3].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "45_to_64years")[0].ToString();
            getDemographics = "SELECT 65_and_over FROM Demographics WHERE zipcode='" + listBox2.SelectedItem.ToString() + "';";
            AgeDistributionZIP.Rows[4].Cells[1].Value = myDB.SQLSELECTExec(getDemographics, "65_and_over")[0].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null && !listBox4.Items.Contains(listBox3.SelectedItem))
                listBox4.Items.Add(listBox3.SelectedItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedItem != null)
                listBox4.Items.Remove(listBox4.SelectedItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // HANDLE NO STATE OR NO CATEGORY SELECTED
            
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a state before continuing.");
                return;
            }
            if (listBox4.Items.Count == 0)
            {
                MessageBox.Show("Please add at least one category.");
                return;
            }
            BusinessSummaryCITY.Rows.Clear();
            BusinessSummarySTATE.Rows.Clear();
            BusinessSummaryZIP.Rows.Clear();
            string getReviewStats, stateAbbrev = stateToAbbrev[comboBox1.SelectedItem.ToString().ToLower()];
            int i = 0;
            //FILL OUT STATE
            foreach (String category in listBox4.Items)
            {
                BusinessSummarySTATE.Rows[0].Cells[0].Value = category;
                getReviewStats = "SELECT COUNT(DISTINCT reviews.bid) FROM reviews WHERE bid IN (SELECT businesses.bid from businesses, catagories WHERE state='" + stateAbbrev + "' AND businesses.bid IN (SELECT bid FROM catagories WHERE cname='" + category + "'));";
                BusinessSummarySTATE.Rows[0].Cells[1].Value = myDB.SQLSELECTExec(getReviewStats, "COUNT(DISTINCT reviews.bid)")[0].ToString();
                getReviewStats = "SELECT AVG(stars) FROM reviews WHERE bid IN (SELECT businesses.bid from businesses, catagories WHERE state='" + stateAbbrev + "' AND businesses.bid IN (SELECT bid FROM catagories WHERE cname='" + category + "'));";
                BusinessSummarySTATE.Rows[0].Cells[2].Value = myDB.SQLSELECTExec(getReviewStats, "AVG(stars)")[0].ToString();
                getReviewStats = "SELECT COUNT(reviews.bid) FROM reviews WHERE bid IN (SELECT businesses.bid from businesses, catagories WHERE state='" + stateAbbrev + "' AND businesses.bid IN (SELECT bid FROM catagories WHERE cname='" + category + "'));";
                BusinessSummarySTATE.Rows[0].Cells[3].Value = Math.Floor(double.Parse((myDB.SQLSELECTExec(getReviewStats, "COUNT(reviews.bid)")[0].ToString()))/double.Parse(BusinessSummarySTATE.Rows[0].Cells[1].Value.ToString()));
                
                /*
                BusinessSummarySTATE.Rows[0].Cells[0].Value = category;
                getReviewStats = "SELECT COUNT(DISTINCT reviews.bid) FROM reviews INNER JOIN businesses ON businesses.bid=reviews.bid INNER JOIN catagories ON catagories.bid=businesses.bid WHERE state='" + stateAbbrev + "' AND cname='" + category + "';";
                BusinessSummarySTATE.Rows[0].Cells[1].Value = myDB.SQLSELECTExec(getReviewStats, "COUNT(DISTINCT reviews.bid)")[0].ToString();
                getReviewStats = "SELECT AVG(reviews.stars) FROM reviews INNER JOIN businesses ON businesses.bid=reviews.bid INNER JOIN catagories ON catagories.bid=businesses.bid WHERE state='" + stateAbbrev + "' AND cname='" + category + "';";
                BusinessSummarySTATE.Rows[0].Cells[2].Value = myDB.SQLSELECTExec(getReviewStats, "AVG(reviews.stars)")[0].ToString();
                getReviewStats = "SELECT COUNT(reviews.bid) FROM reviews INNER JOIN businesses ON businesses.bid=reviews.bid INNER JOIN catagories ON catagories.bid=businesses.bid WHERE state='" + stateAbbrev + "' AND cname='" + category + "';";
                BusinessSummarySTATE.Rows[0].Cells[3].Value = Math.Floor(double.Parse((myDB.SQLSELECTExec(getReviewStats, "COUNT(reviews.bid)")[0].ToString())) / double.Parse(BusinessSummarySTATE.Rows[0].Cells[1].Value.ToString()));
                */


                i++;
                if (i != listBox4.Items.Count)
                    BusinessSummarySTATE.Rows.Add();
            }

            //FILL OUT CITY
            if (listBox1.SelectedItem != null)
            {
                i = 0;
                foreach (String category in listBox4.Items)
                {
                    BusinessSummaryCITY.Rows[0].Cells[0].Value = category;
                    getReviewStats = "SELECT COUNT(DISTINCT reviews.bid) FROM reviews WHERE bid IN (SELECT businesses.bid from businesses, catagories WHERE state='" + stateAbbrev + "' AND  city='" + listBox1.SelectedItem.ToString() + "' AND businesses.bid IN (SELECT bid FROM catagories WHERE cname='" + category + "'));";
                    BusinessSummaryCITY.Rows[0].Cells[1].Value = myDB.SQLSELECTExec(getReviewStats, "COUNT(DISTINCT reviews.bid)")[0].ToString();
                    getReviewStats = "SELECT AVG(stars) FROM reviews WHERE bid IN (SELECT businesses.bid from businesses, catagories WHERE state='" + stateAbbrev + "' AND  city='" + listBox1.SelectedItem.ToString() + "' AND businesses.bid IN (SELECT bid FROM catagories WHERE cname='" + category + "'));";
                    BusinessSummaryCITY.Rows[0].Cells[2].Value = myDB.SQLSELECTExec(getReviewStats, "AVG(stars)")[0].ToString();
                    getReviewStats = "SELECT COUNT(reviews.bid) FROM reviews WHERE bid IN (SELECT businesses.bid from businesses, catagories WHERE state='" + stateAbbrev + "' AND  city='" + listBox1.SelectedItem.ToString() + "' AND businesses.bid IN (SELECT bid FROM catagories WHERE cname='" + category + "'));";
                    BusinessSummaryCITY.Rows[0].Cells[3].Value = Math.Floor(double.Parse((myDB.SQLSELECTExec(getReviewStats, "COUNT(reviews.bid)")[0].ToString())) / double.Parse(BusinessSummarySTATE.Rows[0].Cells[1].Value.ToString()));
                    

                    /*
                    BusinessSummaryCITY.Rows[0].Cells[0].Value = category;
                    getReviewStats = "SELECT COUNT(DISTINCT reviews.bid) FROM reviews INNER JOIN businesses ON businesses.bid=reviews.bid INNER JOIN catagories ON catagories.bid=businesses.bid WHERE state='" + stateAbbrev + "' AND  city='" + listBox1.SelectedItem.ToString() + "' AND cname='" + category + "';";
                    BusinessSummaryCITY.Rows[0].Cells[1].Value = myDB.SQLSELECTExec(getReviewStats, "COUNT(DISTINCT reviews.bid)")[0].ToString();
                    getReviewStats = "SELECT AVG(reviews.stars) FROM reviews INNER JOIN businesses ON businesses.bid=reviews.bid INNER JOIN catagories ON catagories.bid=businesses.bid WHERE state='" + stateAbbrev + "' AND  city='" + listBox1.SelectedItem.ToString() + "' AND cname='" + category + "';";
                    BusinessSummaryCITY.Rows[0].Cells[2].Value = myDB.SQLSELECTExec(getReviewStats, "AVG(reviews.stars)")[0].ToString();
                    getReviewStats = "SELECT COUNT(reviews.bid) FROM reviews INNER JOIN businesses ON businesses.bid=reviews.bid INNER JOIN catagories ON catagories.bid=businesses.bid WHERE state='" + stateAbbrev + "' AND  city='" + listBox1.SelectedItem.ToString() + "' AND cname='" + category + "';";
                    BusinessSummaryCITY.Rows[0].Cells[3].Value = Math.Floor(double.Parse((myDB.SQLSELECTExec(getReviewStats, "COUNT(reviews.bid)")[0].ToString())) / double.Parse(BusinessSummarySTATE.Rows[0].Cells[1].Value.ToString()));
                    */



                    i++;
                    if (i != listBox4.Items.Count)
                        BusinessSummaryCITY.Rows.Add();
                }
            }

            //FILL OUT ZIP
            if (listBox2.SelectedItem != null)
            {
                i = 0;
                foreach (String category in listBox4.Items)
                {
                    
                    BusinessSummaryZIP.Rows[0].Cells[0].Value = category;
                    getReviewStats = "SELECT COUNT(DISTINCT reviews.bid) FROM reviews WHERE bid IN (SELECT businesses.bid from businesses WHERE zipcode='" + listBox2.SelectedItem.ToString() + "' AND state='" + stateAbbrev + "' AND  city='" + listBox1.SelectedItem.ToString() + "' AND businesses.bid IN (SELECT bid FROM catagories WHERE cname='" + category + "'));";
                    BusinessSummaryZIP.Rows[0].Cells[1].Value = myDB.SQLSELECTExec(getReviewStats, "COUNT(DISTINCT reviews.bid)")[0].ToString();
                    getReviewStats = "SELECT AVG(stars) FROM reviews WHERE bid IN (SELECT businesses.bid from businesses, catagories WHERE zipcode='" + listBox2.SelectedItem.ToString() + "' AND state='" + stateAbbrev + "' AND  city='" + listBox1.SelectedItem.ToString() + "' AND businesses.bid IN (SELECT bid FROM catagories WHERE cname='" + category + "'));";
                    BusinessSummaryZIP.Rows[0].Cells[2].Value = myDB.SQLSELECTExec(getReviewStats, "AVG(stars)")[0].ToString();
                    getReviewStats = "SELECT COUNT(reviews.bid) FROM reviews WHERE bid IN (SELECT businesses.bid from businesses, catagories WHERE zipcode='" + listBox2.SelectedItem.ToString() + "' AND state='" + stateAbbrev + "' AND  city='" + listBox1.SelectedItem.ToString() + "' AND businesses.bid IN (SELECT bid FROM catagories WHERE cname='" + category + "'));";
                    BusinessSummaryZIP.Rows[0].Cells[3].Value = Math.Floor(double.Parse((myDB.SQLSELECTExec(getReviewStats, "COUNT(reviews.bid)")[0].ToString())) / double.Parse(BusinessSummarySTATE.Rows[0].Cells[1].Value.ToString()));
                    
                    /*
                    BusinessSummaryZIP.Rows[0].Cells[0].Value = category;
                    getReviewStats = "SELECT COUNT(DISTINCT reviews.bid) FROM reviews INNER JOIN businesses ON businesses.bid=reviews.bid INNER JOIN catagories ON catagories.bid=businesses.bid WHERE state='" + stateAbbrev + "' AND  city='" + listBox1.SelectedItem.ToString() + "' AND zipcode='" + listBox2.SelectedItem.ToString() + "' AND cname='" + category + "';";
                    BusinessSummaryZIP.Rows[0].Cells[1].Value = myDB.SQLSELECTExec(getReviewStats, "COUNT(DISTINCT reviews.bid)")[0].ToString();
                    getReviewStats = "SELECT AVG(reviews.stars) FROM reviews INNER JOIN businesses ON businesses.bid=reviews.bid INNER JOIN catagories ON catagories.bid=businesses.bid WHERE state='" + stateAbbrev + "' AND  city='" + listBox1.SelectedItem.ToString() + "' AND zipcode='" + listBox2.SelectedItem.ToString() + "' AND cname='" + category + "';";
                    BusinessSummaryZIP.Rows[0].Cells[2].Value = myDB.SQLSELECTExec(getReviewStats, "AVG(reviews.stars)")[0].ToString();
                    getReviewStats = "SELECT COUNT(reviews.bid) FROM reviews INNER JOIN businesses ON businesses.bid=reviews.bid INNER JOIN catagories ON catagories.bid=businesses.bid WHERE state='" + stateAbbrev + "' AND  city='" + listBox1.SelectedItem.ToString() + "' AND zipcode='" + listBox2.SelectedItem.ToString() + "' AND cname='" + category + "';";
                    BusinessSummaryZIP.Rows[0].Cells[3].Value = Math.Floor(double.Parse((myDB.SQLSELECTExec(getReviewStats, "COUNT(reviews.bid)")[0].ToString())) / double.Parse(BusinessSummarySTATE.Rows[0].Cells[1].Value.ToString()));
                    */


                    i++;
                    if (i != listBox4.Items.Count)
                        BusinessSummaryZIP.Rows.Add();
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox6.Items.Clear();
            string getCities = "SELECT city FROM Demographics WHERE state='" + comboBox2.SelectedItem.ToString() + "' ORDER BY city;"; //db query to get cities
            List<String> cities = myDB.SQLSELECTExec(getCities, "city");
            foreach (String city in cities)
            {
                listBox6.Items.Add(city);
            }
        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox5.Items.Clear();
            string getZipcodes = "SELECT zipcode FROM Demographics WHERE city='" + listBox6.SelectedItem.ToString() + "' AND state='" + comboBox2.SelectedItem.ToString() + "' ORDER BY zipcode"; //db query to get zipcodes
            List<String> zipcodes = myDB.SQLSELECTExec(getZipcodes, "zipcode");
            foreach (String zipcode in zipcodes)
            {
                listBox5.Items.Add(zipcode);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox8.SelectedItem != null && !listBox7.Items.Contains(listBox8.SelectedItem))
                listBox7.Items.Add(listBox8.SelectedItem);
            updateAttributes();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox7.SelectedItem != null)
                listBox7.Items.Remove(listBox7.SelectedItem);
            updateAttributes();
        }

        private void updateAttributes()
        {
            listBox9.Items.Clear();
            string getAttributes;
            foreach (String catagory in listBox7.Items)
            {
                getAttributes = "SELECT aname FROM attributesandcatagories WHERE cname='" + catagory + "';";
                List<String> attributes = myDB.SQLSELECTExec(getAttributes, "aname");
                foreach(String attribute in attributes)
                {
                    listBox9.Items.Add(attribute);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a state before continuing.");
                return;
            }
            if (listBox7.Items.Count == 0)
            {
                MessageBox.Show("Please add at least one category.");
                return;
            }
            BusinessSearchTable.Rows.Clear();
            string stateAbbrev = stateToAbbrev[comboBox2.SelectedItem.ToString().ToLower()];
            string searchQuery = "SELECT * FROM businesses WHERE state='" + stateAbbrev + "'";
            if (comboBox4.SelectedItem != null)
                searchQuery += " AND avgStars >= " + comboBox4.SelectedItem;
            if (comboBox5.SelectedItem != null)
                searchQuery += " AND avgStars <= " + comboBox5.SelectedItem;
            if (numericUpDown1.Value > 0)
                searchQuery += " AND reviewCount >= " + numericUpDown1.Value;
            if (numericUpDown2.Value > 0)
                searchQuery += " AND reviewCount <= " + numericUpDown2.Value;

            //STATE ONLY
            if (listBox6.SelectedItem == null && listBox5.SelectedItem == null)
            {
                searchQuery += " AND bid IN (SELECT catagories.bid FROM catagories WHERE cname = '" + listBox7.Items[0].ToString() + "'";
                for (int i = 1; i < listBox7.Items.Count; i++)
                {
                    searchQuery += " AND bid IN (SELECT catagories.bid FROM catagories WHERE cname = '" + listBox7.Items[i].ToString() + "'";
                }
                for (int i = 0; i < listBox7.Items.Count; i++)
                    searchQuery += ")";
                searchQuery += ";";
                MessageBox.Show(searchQuery);
                List<String> bnames = myDB.SQLSELECTExec(searchQuery, "bname");
                for (int i = 0; i < bnames.Count - 1; i++)
                    BusinessSearchTable.Rows.Add();
                for (int i = 0; i < bnames.Count; i++)
                {
                    BusinessSearchTable.Rows[i].Cells[0].Value = bnames[i];
                    searchQuery = "SELECT * FROM businesses WHERE state='" + stateAbbrev + "' AND bname='" + bnames[i] + "';";
                    BusinessSearchTable.Rows[i].Cells[1].Value = myDB.SQLSELECTExec(searchQuery, "city")[0].ToString();
                    BusinessSearchTable.Rows[i].Cells[2].Value = stateAbbrev;
                    searchQuery = "SELECT * FROM businesses WHERE state='" + stateAbbrev + "' AND bname='" + bnames[i] + "';";
                    BusinessSearchTable.Rows[i].Cells[3].Value = myDB.SQLSELECTExec(searchQuery, "zipcode")[0].ToString();
                    searchQuery = "SELECT * FROM businesses WHERE state='" + stateAbbrev + "' AND bname='" + bnames[i] + "';";
                    BusinessSearchTable.Rows[i].Cells[4].Value = myDB.SQLSELECTExec(searchQuery, "avgStars")[0].ToString();
                    searchQuery = "SELECT * FROM businesses WHERE state='" + stateAbbrev + "' AND bname='" + bnames[i] + "';";
                    BusinessSearchTable.Rows[i].Cells[5].Value = myDB.SQLSELECTExec(searchQuery, "reviewCount")[0].ToString();
                }

            }

            //CITY
            else if (listBox5.SelectedItem == null)
            {
                searchQuery += " AND city='" + listBox6.SelectedItem + "' AND bid IN (SELECT catagories.bid FROM catagories WHERE cname = '" + listBox7.Items[0].ToString() + "'";
                for (int i = 1; i < listBox7.Items.Count; i++)
                {
                    searchQuery += " AND bid IN (SELECT catagories.bid FROM catagories WHERE cname = '" + listBox7.Items[i].ToString() + "'";
                }
                for (int i = 0; i < listBox7.Items.Count; i++)
                    searchQuery += ")";
                searchQuery += ";";
                MessageBox.Show(searchQuery);
                List<String> bnames = myDB.SQLSELECTExec(searchQuery, "bname");
                for (int i = 0; i < bnames.Count - 1; i++)
                    BusinessSearchTable.Rows.Add();
                for (int i = 0; i < bnames.Count; i++)
                {
                    BusinessSearchTable.Rows[i].Cells[0].Value = bnames[i];
                    BusinessSearchTable.Rows[i].Cells[1].Value = listBox6.SelectedItem;
                    BusinessSearchTable.Rows[i].Cells[2].Value = stateAbbrev;
                    searchQuery = "SELECT * FROM businesses WHERE state='" + stateAbbrev + "' AND bname='" + bnames[i] + "';";
                    BusinessSearchTable.Rows[i].Cells[3].Value = myDB.SQLSELECTExec(searchQuery, "zipcode")[0].ToString();
                    searchQuery = "SELECT * FROM businesses WHERE state='" + stateAbbrev + "' AND bname='" + bnames[i] + "';";
                    BusinessSearchTable.Rows[i].Cells[4].Value = myDB.SQLSELECTExec(searchQuery, "avgStars")[0].ToString();
                    searchQuery = "SELECT * FROM businesses WHERE state='" + stateAbbrev + "' AND bname='" + bnames[i] + "';";
                    BusinessSearchTable.Rows[i].Cells[5].Value = myDB.SQLSELECTExec(searchQuery, "reviewCount")[0].ToString();
                }
            }

            //ZIP
            else
            {
                searchQuery += " AND zipcode='" + listBox5.SelectedItem + "' AND city='" + listBox6.SelectedItem + "' AND bid IN (SELECT catagories.bid FROM catagories WHERE cname = '" + listBox7.Items[0].ToString() + "'";
                for (int i = 1; i < listBox7.Items.Count; i++)
                {
                    searchQuery += " AND bid IN (SELECT catagories.bid FROM catagories WHERE cname = '" + listBox7.Items[i].ToString() + "'";
                }
                for (int i = 0; i < listBox7.Items.Count; i++)
                    searchQuery += ")";
                searchQuery += ";";
                MessageBox.Show(searchQuery);
                List<String> bnames = myDB.SQLSELECTExec(searchQuery, "bname");
                for (int i = 0; i < bnames.Count - 1; i++)
                    BusinessSearchTable.Rows.Add();
                for (int i = 0; i < bnames.Count; i++)
                {
                    BusinessSearchTable.Rows[i].Cells[0].Value = bnames[i];
                    BusinessSearchTable.Rows[i].Cells[1].Value = listBox6.SelectedItem;
                    BusinessSearchTable.Rows[i].Cells[2].Value = stateAbbrev;
                    BusinessSearchTable.Rows[i].Cells[3].Value = listBox5.SelectedItem;
                    searchQuery = "SELECT * FROM businesses WHERE state='" + stateAbbrev + "' AND bname='" + bnames[i] + "';";
                    BusinessSearchTable.Rows[i].Cells[4].Value = myDB.SQLSELECTExec(searchQuery, "avgStars")[0].ToString();
                    searchQuery = "SELECT * FROM businesses WHERE state='" + stateAbbrev + "' AND bname='" + bnames[i] + "';";
                    BusinessSearchTable.Rows[i].Cells[5].Value = myDB.SQLSELECTExec(searchQuery, "reviewCount")[0].ToString();
                }
            }
        }

        private void BusinessSearchTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView table = sender as DataGridView;
            ReviewsVisualizer reviewsVisualizer = new ReviewsVisualizer();
            reviewsVisualizer.setParams(myDB, table.Rows[e.RowIndex].Cells[0].Value.ToString(), table.Rows[e.RowIndex].Cells[1].Value.ToString(), table.Rows[e.RowIndex].Cells[2].Value.ToString(), table.Rows[e.RowIndex].Cells[3].Value.ToString());
            reviewsVisualizer.Show();
        }
    }
}
