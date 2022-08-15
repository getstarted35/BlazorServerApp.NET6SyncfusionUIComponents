using SalesManagementApp.Models.ReportModels;

namespace SalesManagementApp.Services.Contracts
{
    public interface ISalesOrderReportService
    {
        Task<List<GroupedFieldPriceModel>> GetEmployeePricePerMonthData();
    }
}
