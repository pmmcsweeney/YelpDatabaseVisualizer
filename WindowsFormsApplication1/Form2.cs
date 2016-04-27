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
    public partial class ReviewsVisualizer : Form
    {
        public static Dictionary<string, string> stateToAbbrev = new Dictionary<string, string>() { { "alabama", "AL" }, { "alaska", "AK" }, { "arizona", "AZ" }, { "arkansas", "AR" }, { "california", "CA" }, { "colorado", "CO" }, { "connecticut", "CT" }, { "delaware", "DE" }, { "district of columbia", "DC" }, { "florida", "FL" }, { "georgia", "GA" }, { "hawaii", "HI" }, { "idaho", "ID" }, { "illinois", "IL" }, { "indiana", "IN" }, { "iowa", "IA" }, { "kansas", "KS" }, { "kentucky", "KY" }, { "louisiana", "LA" }, { "maine", "ME" }, { "maryland", "MD" }, { "massachusetts", "MA" }, { "michigan", "MI" }, { "minnesota", "MN" }, { "mississippi", "MS" }, { "missouri", "MO" }, { "montana", "MT" }, { "nebraska", "NE" }, { "nevada", "NV" }, { "new hampshire", "NH" }, { "new jersey", "NJ" }, { "new mexico", "NM" }, { "new york", "NY" }, { "north carolina", "NC" }, { "north dakota", "ND" }, { "ohio", "OH" }, { "oklahoma", "OK" }, { "oregon", "OR" }, { "pennsylvania", "PA" }, { "rhode island", "RI" }, { "south carolina", "SC" }, { "south dakota", "SD" }, { "tennessee", "TN" }, { "texas", "TX" }, { "utah", "UT" }, { "vermont", "VT" }, { "virginia", "VA" }, { "washington", "WA" }, { "west virginia", "WV" }, { "wisconsin", "WI" }, { "wyoming", "WY" } };
        public static Dictionary<int, string> catagoryMonicker = new Dictionary<int, string>() { { 1, "d" }, { 2, "e" }, { 3, "f" }, { 4, "g" }, { 5, "h" }, { 6, "i" } };
        public ReviewsVisualizer()
        {
            InitializeComponent();
            
        }

        public void setParams(MySQL_Connection myDB, string bname, string city, string state, string zip)
        {
            string getReviews = "SELECT rid FROM reviews WHERE bid IN (SELECT businesses.bid FROM businesses WHERE bname='" + bname + /*"' AND state='" + state + "' AND city='" + city + "' AND zipcode='" + zip + */"');";
            List<String> reviewIDs = myDB.SQLSELECTExec(getReviews, "rid");
            int i = 0;
            for (i = 0; i < reviewIDs.Count - 1; i++)
                dataGridView1.Rows.Add();
            i = 0;
            foreach (String reviewID in reviewIDs)
            {
                getReviews = "SELECT * FROM reviews WHERE rid='" + reviewID + "';";
                dataGridView1.Rows[i].Cells[0].Value = myDB.SQLSELECTExec(getReviews, "rDate")[0];
                dataGridView1.Rows[i].Cells[1].Value = myDB.SQLSELECTExec(getReviews, "stars")[0];
                dataGridView1.Rows[i].Cells[2].Value = myDB.SQLSELECTExec(getReviews, "rText")[0];
                dataGridView1.Rows[i].Cells[3].Value = myDB.SQLSELECTExec(getReviews, "uName")[0];
                dataGridView1.Rows[i].Cells[4].Value = myDB.SQLSELECTExec(getReviews, "usefulVotes")[0];
                i++;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
