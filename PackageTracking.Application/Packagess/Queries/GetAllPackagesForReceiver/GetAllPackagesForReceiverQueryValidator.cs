using FluentValidation;
using PackageTracking.Application.Packages.Dtos;

namespace PackageTracking.Application.Packagess.Queries.GetAllPackagesForReceiver;
public class GetAllPackagesForReceiverQueryValidator : AbstractValidator<GetAllPackagesForReceiverQuery>
{
    private int[] allowPageSizes = { 10, 15, 20, 25, 30, 50 };
    private string[] allowSortBy = { nameof(PackageDto.Description), nameof(PackageDto.Street) };
    public GetAllPackagesForReceiverQueryValidator()
    {
        RuleFor(x => x.SearchDescription).Length(0, 100);

        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize).Must(x => allowPageSizes.Contains(x))
            .WithMessage($"PageSize must be one of the following values: {string.Join(", ", allowPageSizes)}");

        RuleFor(x => x.SortBy).Must(x => string.IsNullOrEmpty(x) || allowSortBy.Contains(x))
            .When(q => q.SortBy is not null)
            .WithMessage($"Sort by is optional or must be one of the following values: {string.Join(", ", allowSortBy)}");
    }
}
