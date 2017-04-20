using System;
using System.Configuration;

namespace Web.UI.Tests.Environment {
    class TestAccountsConfig : ConfigurationSection {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public EnvironmentElementCollection Elements => (EnvironmentElementCollection)this[""];
    }

    public class EnvironmentElementCollection : ConfigurationElementCollection {
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

        protected override string ElementName => "environment";

        protected override ConfigurationElement CreateNewElement() {
            return new EnvironmentElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            return ((EnvironmentElement)element).Name;
        }
    }

    public class EnvironmentElement : ConfigurationElement {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name => (string)base["name"];

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url => (string)this["url"];

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public AccountDetailsElementCollection Elements => (AccountDetailsElementCollection)this[""];
    }

    public class AccountDetailsElementCollection : ConfigurationElementCollection {
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

        protected override string ElementName => "accountDetails";

        protected override ConfigurationElement CreateNewElement() {
            return new AccountDetailsElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            return ((AccountDetailsElement)element).Name;
        }
    }

    public class AccountDetailsElement : ConfigurationElement {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name => (string)base["name"];

        [ConfigurationProperty("username", IsRequired = true)]
        public string Username => (string)this["username"];

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password => (string)this["password"];
    }
}
