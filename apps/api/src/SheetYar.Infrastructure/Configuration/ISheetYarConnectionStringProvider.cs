namespace SheetYar.Infrastructure.Configuration;

public interface ISheetYarConnectionStringProvider
{
    bool TryGet(out string connectionString);

    string GetRequired();
}
