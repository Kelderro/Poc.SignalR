using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Kelderro.Poc.Data.Entities;

namespace Kelderro.Poc.Data.EntityTypeConfiguration
{
	public class UserMapper : EntityTypeConfiguration<User>
	{
		public UserMapper()
		{
			ToTable("Users");

			HasKey(p => p.Id);
			Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			Property(p => p.Id).IsRequired();

			Property(p => p.FirstName).IsRequired();
			Property(p => p.FirstName).HasMaxLength(255);

			Property(p => p.MiddleName).IsRequired();
			Property(p => p.MiddleName).HasMaxLength(255);

			Property(p => p.LastName).IsRequired();
			Property(p => p.LastName).HasMaxLength(255);
		}
	}

	public class MessageMapper : EntityTypeConfiguration<Message>
	{
		public MessageMapper()
		{
			ToTable("Messages");

			HasKey(p => p.Id);
			HasRequired(p => p.User).WithMany(e => e.Messages).Map(x => x.MapKey("UserId")).WillCascadeOnDelete();
		}
	}
}
