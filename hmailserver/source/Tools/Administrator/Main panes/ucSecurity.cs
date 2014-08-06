// Copyright (c) 2010 Martin Knafve / hMailServer.com.  
// http://www.hmailserver.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using hMailServer.Administrator.Utilities;
using hMailServer.Shared;

namespace hMailServer.Administrator
{
    public partial class ucSecurity : UserControl, ISettingsControl
    {
       public ucSecurity()
        {
            InitializeComponent();

            DirtyChecker.SubscribeToChange(this, OnContentChanged);

            new TabOrderManager(this).SetTabOrder(TabOrderManager.TabScheme.AcrossFirst);
        }

        public void OnLeavePage()
        {

        }

        public bool Dirty
        {
           get
           {
              return DirtyChecker.IsDirty(this);
           }
        }

        public void LoadData()
        {
           hMailServer.Application app = APICreator.Application;

           hMailServer.Settings settings = app.Settings;

           checkVerifyRemoteServerSslCertificate.Checked = settings.VerifyRemoteSslCertificate;
           textSslCipherList.Text = settings.SslCipherList;

           Marshal.ReleaseComObject(settings);
        }

        public bool SaveData()
        {
           hMailServer.Application app = APICreator.Application;

           hMailServer.Settings settings = app.Settings;

           settings.VerifyRemoteSslCertificate = checkVerifyRemoteServerSslCertificate.Checked;
           settings.SslCipherList = textSslCipherList.Text;

           Marshal.ReleaseComObject(settings);

           Utility.AskRestartServer();

           DirtyChecker.SetClean(this);
           return true;

       }

        public void LoadResources()
        {
            // load the translated resources
        }


        private void OnContentChanged()
        {
           Instances.MainForm.OnContentChanged();
        }

        private void OnContentChanged(object sender, EventArgs e)
        {
           OnContentChanged();
        }
    }
}