using Microsoft.EntityFrameworkCore;
using SheetYar.Domain.Templates;

namespace SheetYar.Infrastructure.Persistence;

public sealed class DatabaseSeeder(SheetYarDbContext dbContext)
{
    private static readonly TemplateSeed[] TemplateSeeds =
    [
        new(
            Guid.Parse("af810099-2991-41d8-a284-a70801ae2c44"),
            "budget-planner",
            "Budget Planner",
            "Plan income, expenses, and monthly balances.",
            10),
        new(
            Guid.Parse("55353a50-e0b0-464d-b1af-52cbedc62554"),
            "expense-tracker",
            "Expense Tracker",
            "Record and categorize everyday expenses.",
            20),
        new(
            Guid.Parse("6f7308a1-0198-4bc0-af9b-5ab21860bbcb"),
            "inventory-tracker",
            "Inventory Tracker",
            "Track inventory quantities, costs, and reorder levels.",
            30),
    ];

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        var codes = TemplateSeeds.Select(seed => seed.Code).ToArray();
        var existingTemplates = await dbContext.Templates
            .Where(template => codes.Contains(template.Code))
            .ToDictionaryAsync(template => template.Code, StringComparer.Ordinal, cancellationToken);

        foreach (var seed in TemplateSeeds)
        {
            if (!existingTemplates.TryGetValue(seed.Code, out var template))
            {
                dbContext.Templates.Add(new Template
                {
                    Id = seed.Id,
                    Code = seed.Code,
                    Name = seed.Name,
                    Description = seed.Description,
                    IsActive = true,
                    SortOrder = seed.SortOrder,
                });
                continue;
            }

            template.Name = seed.Name;
            template.Description = seed.Description;
            template.IsActive = true;
            template.SortOrder = seed.SortOrder;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private sealed record TemplateSeed(
        Guid Id,
        string Code,
        string Name,
        string Description,
        int SortOrder);
}
