using System.Collections.Generic;
using FluentValidation;
using NzbDrone.Core.Annotations;
using NzbDrone.Core.ThingiProvider;
using NzbDrone.Core.Validation;

namespace NzbDrone.Core.Notifications.Email
{
    public class EmailSettingsValidator : AbstractValidator<EmailSettings>
    {
        public EmailSettingsValidator()
        {
            RuleFor(c => c.Server).NotEmpty();
            RuleFor(c => c.Port).InclusiveBetween(1, 65535);
            RuleFor(c => c.From).NotEmpty();
            RuleFor(c => c.To).NotEmpty();
        }
    }

    public class EmailSettings : IProviderConfig
    {
        private static readonly EmailSettingsValidator Validator = new EmailSettingsValidator();

        public EmailSettings()
        {
            Port = 25;

            To = new string[] { };
            CC = new string[] { };
            Bcc = new string[] { };
        }

        [FieldDefinition(0, Label = "Server", HelpText = "Hostname or IP of Email server")]
        public string Server { get; set; }

        [FieldDefinition(1, Label = "Port")]
        public int Port { get; set; }

        [FieldDefinition(2, Label = "SSL", Type = FieldType.Checkbox)]
        public bool Ssl { get; set; }

        [FieldDefinition(3, Label = "Username", Privacy = PrivacyLevel.UserName)]
        public string Username { get; set; }

        [FieldDefinition(4, Label = "Password", Type = FieldType.Password, Privacy = PrivacyLevel.Password)]
        public string Password { get; set; }

        [FieldDefinition(5, Label = "From Address")]
        public string From { get; set; }

        [FieldDefinition(6, Label = "Recipient Address(es)", HelpText = "Comma seperated list of email recipients")]
        public IEnumerable<string> To { get; set; }

        [FieldDefinition(7, Label = "CC Address(es)", HelpText = "Comma seperated list of email cc recipients", Advanced = true)]
        public IEnumerable<string> CC { get; set; }

        [FieldDefinition(8, Label = "BCC Address(es)", HelpText = "Comma seperated list of email bcc recipients", Advanced = true)]
        public IEnumerable<string> Bcc { get; set; }

        [FieldDefinition(9, Label = "Attach Books", HelpText = "Add books as an attachment on import", Type = FieldType.Checkbox)]
        public bool AttachFiles { get; set; }

        public NzbDroneValidationResult Validate()
        {
            return new NzbDroneValidationResult(Validator.Validate(this));
        }
    }
}
