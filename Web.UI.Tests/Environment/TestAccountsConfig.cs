using System;
using System.Configuration;

namespace Web.UI.Tests.Environment
{
    public class ConfigHandler : ConfigurationSection
    {
        [ConfigurationProperty("testAccounts")]
        public TestAccountsCollection TestAccounts => ((TestAccountsCollection)(this["testAccounts"]));
    }

    [ConfigurationCollection(typeof(AccountDetails), AddItemName = "accountDetails")]
    public class TestAccountsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AccountDetails();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AccountDetails)element).Name;
        }

        public new AccountDetails this[string key] => (AccountDetails)BaseGet(key);
    }

    public class AccountDetails : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name => (string)base["name"];

        [ConfigurationProperty("username")]
        public string Username => (string)this["username"];

        [ConfigurationProperty("password")]
        public string Password => (string)this["password"];
    }
}