using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Storeme.Data;
using Storeme.Entities;
using Storeme.Services.Contracts;
using System.Drawing;

namespace Storeme.Services.Implementations
{
    public class OrderService : DataService, IOrderService
    {
        private readonly ICartService cartService;
        public OrderService(StoremeDbContext context, ICartService cartService) : base(context)
        {
            this.cartService = cartService;
        }


        public async Task<bool> CreateOrder(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            var cart = await this.cartService.UserCart(username);

            try
            {
                if (user != null)
                {
                    var itemsList = cart.Items.ToList();
                    foreach (var item in itemsList)
                    {
                        var product = await this.context.Products.FirstAsync(x => x.Id == item.Item.ProductId);
                        if (product != null && product.Quantity >= 1)
                        {

                            var order = new Order()
                            {
                                Quantity = 1,
                                IsFinished = false,
                                CreatedOn = DateTime.Now,
                                UserId = user.Id,
                                User = user,
                                ProductId = item.Item.ProductId,
                                Product = item.Item.Product
                            };

                            product.Quantity -= order.Quantity;
                            this.context.Products.Update(product);
                            await this.context.Orders.AddAsync(order);
                            this.context.SaveChanges();
                        }
                        else
                        {
                            return false;
                        }

                    }
                    foreach (var item in itemsList)
                    {
                        await this.cartService.RemoveItemFromCart(item.Item.ProductId, username);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await this.context.Orders
                .Include(x => x.User)
                .Include(x => x.Product)
                .ThenInclude(c => c.Category)
                .ToListAsync();
        }

        public async Task<List<Order>> UserOrders(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            var orders = new List<Order>();

            if (user != null)
            {
                orders = await this.context.Orders.Where(x => x.UserId == user.Id)
                    .Include(x => x.User)
                    .Include(x => x.Product)
                    .ThenInclude(c => c.Category)
                    .ToListAsync();
            }
            return orders;
        }

        public async Task<MemoryStream> ExportOrders(string username, List<Order> orders)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            var stream = new MemoryStream();
            if (user != null)
            {
                using (var xlPackage = new ExcelPackage(stream))
                {
                    var worksheet = xlPackage.Workbook.Worksheets.Add("Orders_" + user.UserName);
                    var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                    namedStyle.Style.Font.UnderLine = true;
                    namedStyle.Style.Font.Color.SetColor(Color.Blue);
                    const int startRow = 5;
                    var row = startRow;

                    worksheet.Cells["A1"].Value = user.UserName + " Orders";
                    using (var r = worksheet.Cells["A1:H1"])
                    {
                        r.Merge = true;
                        r.Style.Font.Color.SetColor(Color.White);
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                    }

                    worksheet.Cells["A4"].Value = "Delivered";
                    worksheet.Cells["B4"].Value = "UserName";
                    worksheet.Cells["C4"].Value = "Brand";
                    worksheet.Cells["D4"].Value = "Model";
                    worksheet.Cells["E4"].Value = "Category";
                    worksheet.Cells["F4"].Value = "Quantity";
                    worksheet.Cells["G4"].Value = "Unit Price (Euro)";
                    worksheet.Cells["H4"].Value = "Total Price (Euro)";
                    worksheet.Cells["A4:H4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["A4:H4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                    worksheet.Cells["A4:H4"].Style.Font.Bold = true;

                    row = 5;
                    foreach (var order in orders)
                    {
                        worksheet.Cells[row, 1].Value = order.IsFinished;
                        worksheet.Cells[row, 2].Value = order.User.UserName;
                        worksheet.Cells[row, 3].Value = order.Product.Brand;
                        worksheet.Cells[row, 4].Value = order.Product.DeviceModel;
                        worksheet.Cells[row, 5].Value = order.Product.Category.Title;
                        worksheet.Cells[row, 6].Value = order.Quantity;
                        worksheet.Cells[row, 7].Value = order.Product.Price;
                        worksheet.Cells[row, 8].Value = order.Quantity * order.Product.Price;
                        row++;
                    }

                    xlPackage.Workbook.Properties.Title = "Orders List";
                    xlPackage.Workbook.Properties.Author = user.UserName;
                    xlPackage.Workbook.Properties.Subject = user.UserName + " List";
                    xlPackage.Save();

                }
                stream.Position = 0;
            }
            return stream;
        }

        public async Task<StoremeUser> GetUser(string username)
        {
            return await this.context.Users
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

    }
}
