using Microsoft.EntityFrameworkCore;
using SalesManagementApp.Data;
using SalesManagementApp.Models.ReportModels;
using SalesManagementApp.Services.Contracts;

namespace SalesManagementApp.Services
{
    public class SalesOrderReportService : ISalesOrderReportService
    {
        private readonly SalesManagementDbContext salesManagementDbContext;

        public SalesOrderReportService(SalesManagementDbContext salesManagementDbContext)
        {
            this.salesManagementDbContext = salesManagementDbContext;
        }
        public async Task<List<GroupedFieldPriceModel>> GetEmployeePricePerMonthData()
        {
            try
            {
                var reportData= await (from s in this.salesManagementDbContext.SalesOrderReports
                                       where s.EmployeeId == 9
                                      group s by s.OrderDateTime.Month into GroupedData
                                       orderby GroupedData.Key
                                       select new GroupedFieldPriceModel
                                       {
                                           GroupedFieldKey=(
                                           GroupedData.Key == 1 ? "Jan":
                                           GroupedData.Key == 2 ? "Feb" :
                                           GroupedData.Key == 3 ? "Mar" :
                                           GroupedData.Key == 4 ? "Apr" :
                                           GroupedData.Key == 5 ? "May" :
                                           GroupedData.Key == 6 ? "Jun" :
                                           GroupedData.Key == 7 ? "Jul" :
                                           GroupedData.Key == 8 ? "Ags" :
                                           GroupedData.Key == 9 ? "Sep" :
                                           GroupedData.Key == 10 ? "Oct" :
                                           GroupedData.Key == 11 ? "Nov" :
                                           GroupedData.Key == 12 ? "Dec" :
                                           ""
                                           ),
                                           Price =Math.Round (GroupedData.Sum(o => o.OrderItemPrice),2)

                                       }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
