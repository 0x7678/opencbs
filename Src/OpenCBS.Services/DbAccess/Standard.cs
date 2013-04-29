using System;
using Octopus.Services.Accounting;
using Octopus.Services.Currencies;
using Octopus.Services.Events;
using Octopus.Services.Rules;
using Octopus.Shared;
using Octopus.Shared.Settings;
using Reports = Octopus.Enums.OReports;
using Octopus.CoreDomain;
using Octopus.Services.Export;

namespace Octopus.Services
{
    public class Standard : IServices
    {
        private static User CurrentUser
        {
            get { return User.CurrentUser; }
        }

        public UserServices GetUserServices()
        {
            Console.WriteLine("UserServices cot� client");
            return new UserServices(CurrentUser);
        }

        public AccountingServices GetAccountingServices()
        {
            Console.WriteLine("AccountingServices cot� client");
            return new AccountingServices(CurrentUser);
        }
        public RoleServices GetRoleServices()
        {
            Console.WriteLine("RoleServices cot� client");
            return new RoleServices(CurrentUser);
        }
        public RegExCheckerServices GetRegExCheckerServices()
        {
            Console.WriteLine("RegExCheckerServices cot� client");
            return new RegExCheckerServices(CurrentUser);
        }
        public ExchangeRateServices GetExchangeRateServices()
        {
            Console.WriteLine("ExchangeRateServices cot� client");
            return new ExchangeRateServices(CurrentUser);
        }

        //Permet d'initialiser le service "SavingProduct" en local
        public SavingProductServices GetSavingProductServices()
        {
            Console.WriteLine("SavingProductServices cot� client");
            return new SavingProductServices(CurrentUser);
        }

        public SavingServices GetSavingServices()
        {
            Console.WriteLine("SavingServices cot� client");
            return new SavingServices(CurrentUser);
        }

        public ChartOfAccountsServices GetChartOfAccountsServices()
        {
            Console.WriteLine("GlobalAccountingParametersServices cot� client");
            return new ChartOfAccountsServices(CurrentUser);
        }

        public StandardBookingServices GetStandardBookingServices()
        {
            Console.WriteLine("StandardBookingServices cot� client");
            return new StandardBookingServices(CurrentUser);
        }

        public EventProcessorServices GetEventProcessorServices()
        {
            Console.WriteLine("EventProcessorServices cot� client");
            return new EventProcessorServices(CurrentUser);
        }

        //public CashReceiptServices GetCashReceiptServices()
        //{
        //    Console.WriteLine("CashReceiptServices cot� client");
        //    return new CashReceiptServices(User.CurrentUser);
        //}

        public ClientServices GetClientServices()
        {
            Console.WriteLine("ClientServices cot� client");
            return new ClientServices(CurrentUser);
        }

        public LoanServices GetContractServices()
        {
            Console.WriteLine("ContractServices cot� client");
            return new LoanServices(CurrentUser);
        }

        public DatabaseServices GetDatabaseServices()
        {
            Console.WriteLine("DatabaseServices cot� client");
            return new DatabaseServices(); 
        }

        public EconomicActivityServices GetEconomicActivityServices()
        {
            Console.WriteLine("EconomicActivities cot� client");
            return new EconomicActivityServices(CurrentUser);
        }

        public ApplicationSettingsServices GetApplicationSettingsServices()
        {
            Console.WriteLine("GeneralSettingsServices cot� client");
            return new ApplicationSettingsServices(CurrentUser);
        }

        public GraphServices GetGraphServices()
        {
            Console.WriteLine("GraphServices cot� client");
            return new GraphServices(CurrentUser);
        }

        public LocationServices GetLocationServices()
        {
            return new LocationServices(CurrentUser);
        }

        public PicturesServices GetPicturesServices()
        {
            return new PicturesServices(CurrentUser);
        }

        public ProductServices GetProductServices()
        {
            return new  ProductServices(CurrentUser);
        }

        public CollateralProductServices GetCollateralProductServices()
        {
            return new CollateralProductServices(CurrentUser);
        }

        public MFIServices GetMFIServices()
        {
            return new MFIServices(CurrentUser);
        }

        public SettingsImportExportServices GetSettingsImportExportServices()
        {
            return new SettingsImportExportServices(CurrentUser);
        }

        public ProjectServices GetProjectServices()
        {
            return new ProjectServices(CurrentUser);
        }

        public PaymentMethodServices GetPaymentMethodServices()
        {
            return new PaymentMethodServices(CurrentUser);
        }

        public MenuItemServices GetMenuItemServices()
        {
            return new MenuItemServices(CurrentUser);
        }

        public string GetAuthentification(string pOctoUsername, string pOctoPass, string pDbName, string pComputerName, string pLoginName)
        {
            throw new NotImplementedException();
        }

        public void RunTimeout()
        {
            throw new NotImplementedException();
        }

        public string GetToken()
        {
            return "";
        }
     
        public ApplicationSettings GetGeneralSettings()
        {
            return ApplicationSettings.GetInstance("");
        }

        public FundingLineServices GetFundingLinesServices()
        {
            return new FundingLineServices(CurrentUser);
        }

        public SQLToolServices GetSQLToolServices()
        {
            return new SQLToolServices(CurrentUser);
        }

        public CurrencyServices GetCurrencyServices()
        {
            return new CurrencyServices(CurrentUser);
        }

        public QuestionnaireServices GetQuestionnaireServices()
        {
            return new QuestionnaireServices(CurrentUser);
        }

        public AccountingRuleServices GetAccountingRuleServices()
        {
            return new AccountingRuleServices(CurrentUser);
        }

        public ExportServices GetExportServices()
        {
            return new ExportServices(CurrentUser);
        }

        public BranchService GetBranchService()
        {
            return new BranchService(CurrentUser);
        }

        public TellerServices GetTellerServices()
        {
            return new TellerServices(CurrentUser);
        }

        public CustomizableFieldsServices GetCustomizableFieldsServices()
        {
            return new CustomizableFieldsServices(CurrentUser);
        }

        #region IServices Members

        public NonWorkingDateSingleton GetNonWorkingDate()
        {
            return NonWorkingDateSingleton.GetInstance(CurrentUser.Md5);
        }

        #endregion

        #region IServices Members


        public void SuppressAllRemotingInfos(string pComputerName, string pLoginName)
        {
        }

        #endregion

        #region IServices Members

       #endregion
    }
}
