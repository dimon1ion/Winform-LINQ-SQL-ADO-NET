using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using MoreLinq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Winform_LINQ_SQL_ADO_NET
{
    public partial class Form1 : Form
    {
        ContinentsDataContext dataContext;
        public string connectionString;
        public Form1()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["Winform_LINQ_SQL_ADO_NET.Properties.Settings.WorldConnectionString"].ConnectionString;

            dataContext = new ContinentsDataContext();

            button2.Enabled = false;
            button3.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            toolTip1.SetToolTip(button3, "Work with \"Select Continents\"");


            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedItem = comboBox1.Items[0];
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            bool towns = true, countries = true, continents = true;
            DataTable checkdata = new DataTable();
            string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = 'World'";

            SqlConnection connection = new SqlConnection(connectionString);
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                adapter.Fill(checkdata);
            }

            List<string> arr = new List<string>();

            foreach (DataRow row in checkdata.Rows)
            {
                arr.Add(row.ItemArray[0].ToString());
            }

            if (arr.All(r => r != "Towns"))
            {
                query = "CREATE TABLE Towns( " +
                    "Id int PRIMARY KEY IDENTITY(1,1), Name nvarchar(100) NOT NULL, " +
                    "CountryId int FOREIGN KEY REFERENCES Countries(Id), " +
                    "Population BIGINT NOT NULL, " +
                    "Capital bit NOT NULL Default(0)); ";
                towns = false;
                if (arr.All(r => r != "Countries"))
                {
                    query = "CREATE TABLE Countries( " +
                        "Id int PRIMARY KEY IDENTITY(1,1), " +
                        "Name nvarchar(100) NOT NULL, " +
                        "ContinentId int FOREIGN KEY REFERENCES Continents(Id), " +
                        "Area BIGINT NOT NULL); " + query;
                    countries = false;
                    if (arr.All(r => r != "Continents"))
                    {
                        query = " CREATE TABLE Continents(" +
                            "Id int PRIMARY KEY IDENTITY(1,1), " +
                            "Name nvarchar(100) NOT NULL ); " + query;
                        continents = false;
                    }
                }
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                await command.ExecuteNonQueryAsync();

                connection.Close();
            }

            dataContext = new ContinentsDataContext();
            if (!continents)
            {
                dataContext.Continents.InsertOnSubmit(new Continent() { Name = "Eurasia" });
                dataContext.Continents.InsertOnSubmit(new Continent() { Name = "Africa" });
                dataContext.Continents.InsertOnSubmit(new Continent() { Name = "North America" });
                dataContext.Continents.InsertOnSubmit(new Continent() { Name = "South America" });
                dataContext.Continents.InsertOnSubmit(new Continent() { Name = "Australia" });
                dataContext.Continents.InsertOnSubmit(new Continent() { Name = "Antarctica" });

                dataContext.SubmitChanges();
            }
            if (!countries)
            {
                int id;

                if (dataContext.Continents.Where(c => c.Name == "Eurasia").FirstOrDefault() != null)
                {
                    //Eurasia id
                    id = dataContext.Continents.Where(c => c.Name == "Eurasia").FirstOrDefault().Id;

                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Russia", Area = 17130000, ContinentId = id });
                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Azerbaijan", Area = 86600, ContinentId = id });
                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Turkey", Area = 783562, ContinentId = id });
                }

                if (dataContext.Continents.Where(c => c.Name == "Africa").FirstOrDefault() != null)
                {
                    //Africa id
                    id = dataContext.Continents.Where(c => c.Name == "Africa").FirstOrDefault().Id;

                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Nigeria", Area = 923768, ContinentId = id });
                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Egypt", Area = 1010000, ContinentId = id });
                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Uganda", Area = 241037, ContinentId = id });
                }

                if (dataContext.Continents.Where(c => c.Name == "North America").FirstOrDefault() != null)
                {
                    //North America id
                    id = dataContext.Continents.Where(c => c.Name == "North America").FirstOrDefault().Id;

                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Canada", Area = 9985000, ContinentId = id });
                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Mexico", Area = 1973000, ContinentId = id });
                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "USA", Area = 9834000, ContinentId = id });
                }

                if (dataContext.Continents.Where(c => c.Name == "South America").FirstOrDefault() != null)
                {
                    //South America id
                    id = dataContext.Continents.Where(c => c.Name == "South America").FirstOrDefault().Id;

                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Argentina", Area = 2780000, ContinentId = id });
                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Bolivia", Area = 1099000, ContinentId = id });
                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Brazil", Area = 8516000, ContinentId = id });
                }

                if (dataContext.Continents.Where(c => c.Name == "Australia").FirstOrDefault() != null)
                {
                    //Australia id
                    id = dataContext.Continents.Where(c => c.Name == "Australia").FirstOrDefault().Id;

                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Australia", Area = 7692000, ContinentId = id });
                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Fiji", Area = 18274, ContinentId = id });
                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Kiribati", Area = 811, ContinentId = id });
                }

                if (dataContext.Continents.Where(c => c.Name == "Antarctica").FirstOrDefault() != null)
                {
                    //Antarctica id
                    id = dataContext.Continents.Where(c => c.Name == "Antarctica").FirstOrDefault().Id;

                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "France", Area = 543940, ContinentId = id });
                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "United Kingdom", Area = 242495, ContinentId = id });
                    dataContext.Countries.InsertOnSubmit(new Country() { Name = "Norway", Area = 385207, ContinentId = id });
                }

                dataContext.SubmitChanges();
            }
            if (!towns)
            {
                int id;

                if (dataContext.Countries.Where(c => c.Name == "Russia").FirstOrDefault() != null)
                {
                    //Russia id
                    id = dataContext.Countries.Where(c => c.Name == "Russia").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Moscow", CountryId = id, Population = 11920000, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Mineralniye vodi", Population = 73183, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Nevinomissk", Population = 117949, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Nalchik", Population = 239230, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Kaliningrad", Population = 437456, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Azerbaijan").FirstOrDefault() != null)
                {
                    //Azerbaijan id
                    id = dataContext.Countries.Where(c => c.Name == "Azerbaijan").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Baku", Population = 2236000, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Sheki", Population = 68400, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Ganja", Population = 330744, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Shirvan", Population = 87400, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Shamakhi", Population = 31704, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Turkey").FirstOrDefault() != null)
                {
                    //Turkey id
                    id = dataContext.Countries.Where(c => c.Name == "Turkey").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Ankara", Population = 5663000, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Izmir", Population = 4367000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Bodrum", Population = 36401, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Antalia", Population = 1203994, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Stambul", Population = 15460000, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Nigeria").FirstOrDefault() != null)
                {
                    //Nigeria id
                    id = dataContext.Countries.Where(c => c.Name == "Nigeria").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Abudja", Population = 1236000, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Sapele", Population = 231549, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Jimeta", Population = 286001, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Lagos", Population = 21324000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Kano", Population = 3848885, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Egypt").FirstOrDefault() != null)
                {
                    //Egypt id
                    id = dataContext.Countries.Where(c => c.Name == "Egypt").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Kair", Population = 9540000, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Hurgada", Population = 400901, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Aleksandriya", Population = 5200000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Giza", Population = 8800000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Asuan", Population = 1568000, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Uganda").FirstOrDefault() != null)
                {
                    //Uganda id
                    id = dataContext.Countries.Where(c => c.Name == "Uganda").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Kampala", Population = 1507000, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Gulu", Population = 152276, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Lira", Population = 99059, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Jinja", Population = 300000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Kasese", Population = 101679, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Canada").FirstOrDefault() != null)
                {
                    //Canada id
                    id = dataContext.Countries.Where(c => c.Name == "Canada").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Ottava", Population = 994837, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Monreal", Population = 1780000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Toronto", Population = 2930000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Kalgari", Population = 1336000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Vinnipeg", Population = 749534, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Mexico").FirstOrDefault() != null)
                {
                    //Mexico id
                    id = dataContext.Countries.Where(c => c.Name == "Mexico").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Mehico", Population = 8855000, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Verakrus", Population = 702394, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Puebla", Population = 5779829, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Kankun", Population = 888797, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Merida", Population = 892363, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "USA").FirstOrDefault() != null)
                {
                    //USA id
                    id = dataContext.Countries.Where(c => c.Name == "USA").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Vashington", Population = 692683, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "New-York", Population = 8419000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Los-Angeles", Population = 3967000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Chikago", Population = 2710000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "San-Francisko", Population = 874961, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Argentina").FirstOrDefault() != null)
                {
                    //Argentina id
                    id = dataContext.Countries.Where(c => c.Name == "Argentina").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Buenos-Ayres", Population = 3063728, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Salta", Population = 535303, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Kordova", Population = 326039, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Mendosa", Population = 1115041, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Katamarka", Population = 396895, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Bolivia").FirstOrDefault() != null)
                {
                    //Bolivia id
                    id = dataContext.Countries.Where(c => c.Name == "Bolivia").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "La-pas", Population = 766468, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Sukre", Population = 300000, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Potosi", Population = 240966, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Oruro", Population = 490612, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Kochabamba", Population = 1861924, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Brazil").FirstOrDefault() != null)
                {
                    //Brazil id
                    id = dataContext.Countries.Where(c => c.Name == "Brazil").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Brazil", Population = 2609997, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Fortaleza", Population = 2687000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Manaus", Population = 2020000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Salvador", Population = 6486000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Goyaniya", Population = 1536097, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Australia").FirstOrDefault() != null)
                {
                    //Australia id
                    id = dataContext.Countries.Where(c => c.Name == "Australia").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Kanberra", Population = 395790, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Sidney", Population = 5312000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Melburn", Population = 5078000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Brisben", Population = 2280000, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Pert", Population = 1985000, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Fiji").FirstOrDefault() != null)
                {
                    //Fiji id
                    id = dataContext.Countries.Where(c => c.Name == "Fiji").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Suva", Population = 93970, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Nandi", Population = 42284, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Savusavu", Population = 3372, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Levuka", Population = 1131, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Labasa", Population = 27949, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Kiribati").FirstOrDefault() != null)
                {
                    //Kiribati id
                    id = dataContext.Countries.Where(c => c.Name == "Kiribati").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Tarava", Population = 56284, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Rungada", Population = 847, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Eita", Population = 4979, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Tabvakeya", Population = 3537, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Banana", Population = 1208, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "France").FirstOrDefault() != null)
                {
                    //France id
                    id = dataContext.Countries.Where(c => c.Name == "France").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Paris", Population = 2161000, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Marcel", Population = 861635, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Lion", Population = 513275, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Tuluza", Population = 471941, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Nizza", Population = 342522, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "United Kingdom").FirstOrDefault() != null)
                {
                    //United Kingdom id
                    id = dataContext.Countries.Where(c => c.Name == "United Kingdom").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "London", Population = 8982000, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Manchester", Population = 553230, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Oksford", Population = 152450, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Kembridge", Population = 123900, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "York", Population = 208400, CountryId = id });
                }

                if (dataContext.Countries.Where(c => c.Name == "Norway").FirstOrDefault() != null)
                {
                    //Norway id
                    id = dataContext.Countries.Where(c => c.Name == "Norway").FirstOrDefault().Id;

                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Oslo", Population = 634293, CountryId = id, Capital = true });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Bergen", Population = 271949, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Stavanger", Population = 130754, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Tryumse", Population = 71590, CountryId = id });
                    dataContext.Towns.InsertOnSubmit(new Town() { Name = "Moss", Population = 31308, CountryId = id });
                }

                dataContext.SubmitChanges();
            }

            labelVerify.ForeColor = Color.Green;
            labelVerify.Text = "verified";
            button2.Enabled = true;
            button3.Enabled = true;
            comboBox1.Enabled = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    //dataGridView1.DataSource = from continent in dataContext.Continents select new { continent.Id, continent.Name };
                    dataGridView1.DataSource = dataContext.Continents; // Update здесь работает т.к. передаем ссылку
                    break;
                case 1:
                    // В этом случае не работает, потому что создается отдельная коллекция или заполняются копии данных по очереди в dataGridView, но я выбрал красивую таблицу вместо функции UPDATE
                    dataGridView1.DataSource = from country in dataContext.Countries select new { country.Id, country.Name, country.Area };
                    break;
                case 2:
                    if (comboBox2.SelectedItem != null)
                    {
                        dataGridView1.DataSource = from country in dataContext.Countries.Where(c => c.Continent.Name == comboBox2.Text) select new { country.Id, country.Name, country.Area };
                    }
                    break;
                case 3:
                    if (comboBox3.SelectedItem != null)
                    {
                        dataGridView1.DataSource = from town in dataContext.Towns.Where(c => c.Country.Name == comboBox3.Text) select new { town.Id, town.Name, town.Population, town.Capital };
                    }
                    break;
                case 4:
                    dataGridView1.DataSource = from towns in dataContext.Towns select new { towns.Id, towns.Name, towns.Population, towns.Capital };
                    break;
                case 5:
                    dataGridView1.DataSource = (from country in dataContext.Countries
                                               orderby country.Area 
                                               descending select new { 
                                                   country.Id, country.Name, country.Area 
                                               }).Take(5);
                    break;
                case 6:
                    dataGridView1.DataSource = (from country in dataContext.Countries
                                               orderby country.Towns.Sum(t => t.Population) descending
                                               select new
                                               {
                                                   country.Id,
                                                   country.Name,
                                                   country.Area,
                                                   Population = country.Towns.Sum(t => t.Population)
                                               }).Take(5);
                    break;
                case 7:
                    dataGridView1.DataSource = (from town in dataContext.Towns
                                                orderby town.Population descending
                                                select new
                                                {
                                                    town.Id,
                                                    town.Name,
                                                    town.Population
                                                }).Take(5);
                    break;
                case 8:
                    dataGridView1.DataSource = (from continent in dataContext.Continents
                                                orderby continent.Countries.Sum(c => c.Area) descending
                                                select new
                                                {
                                                    continent.Id,
                                                    continent.Name,
                                                    Area = continent.Countries.Sum(c => c.Area)
                                                }).Take(3);
                    break;
                case 9:
                    dataGridView1.DataSource = (from continent in dataContext.Continents
                                                orderby (from country in continent.Countries
                                                         select new { Population = country.Towns.Sum(t => t.Population) }).Sum(c => c.Population) descending
                                                select new
                                                {
                                                    continent.Id,
                                                    continent.Name,
                                                    Population = (from country in continent.Countries
                                                                  select new { Population = country.Towns.Sum(t => t.Population) }).Sum(c => c.Population)
                                                }).Take(3);
                    break;
                default:
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            switch (comboBox1.SelectedIndex)
            {
                case 2:
                    comboBox2.Items.Clear();
                    foreach (Continent continent in dataContext.Continents)
                    {
                        comboBox2.Items.Add(continent.Name);
                    }
                    if (comboBox2.Items.Count > 0)
                    {
                        comboBox2.SelectedIndex = 0;
                    }
                    comboBox2.Visible = true;
                    break;
                case 3:
                    int count = 0;
                    while (true)
                    {
                        try
                        {
                            comboBox3.Items.Clear();
                            foreach (Country country in dataContext.Countries)
                            {
                                comboBox3.Items.Add(country.Name.ToString());
                            }
                            if (comboBox3.Items.Count > 0)
                            {
                                comboBox3.SelectedIndex = 0;
                            }
                            break;
                        }
                        catch (Exception)
                        {
                            if (count > 200)
                            {
                                MessageBox.Show("Timeout");
                                break;
                            }
                            count++;
                            continue;
                        }
                    }
                    comboBox3.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataContext.SubmitChanges();
        }
    }
}
