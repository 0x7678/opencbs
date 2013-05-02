﻿// LICENSE PLACEHOLDER

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.Services;

namespace OpenCBS.GUI
{
    /// <summary>
    /// Summary description for CityForm.
    /// </summary>
    public class CityForm : Form
    {
        private ListView listViewCity;
        private ColumnHeader columnHeader1;
        private string city;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public CityForm(int districtId)
        {
            InitializeComponent();
            InitializeCities(districtId);
        }

        public string City
        {
            get { return city; }
        }

        private void InitializeCities(int districtId)
        {
            List<City> cities = ServicesProvider.GetInstance().GetLocationServices().FindCityByDistrictId(districtId);
            listViewCity.Items.Clear();
            foreach (City cityObject in cities)
            {
                ListViewItem listViewItem = new ListViewItem(cityObject.Name);
                listViewCity.Items.Add(listViewItem);
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CityForm));
            this.listViewCity = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listViewCity
            // 
            this.listViewCity.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            resources.ApplyResources(this.listViewCity, "listViewCity");
            this.listViewCity.FullRowSelect = true;
            this.listViewCity.GridLines = true;
            this.listViewCity.Name = "listViewCity";
            this.listViewCity.UseCompatibleStateImageBehavior = false;
            this.listViewCity.View = System.Windows.Forms.View.Details;
            this.listViewCity.DoubleClick += new System.EventHandler(this.listViewCity_DoubleClick);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // CityForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.listViewCity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CityForm";
            this.ResumeLayout(false);

        }

        #endregion

        private void listViewCity_DoubleClick(object sender, EventArgs e)
        {
            city = listViewCity.SelectedItems[0].Text;
            Close();
        }
    }
}
