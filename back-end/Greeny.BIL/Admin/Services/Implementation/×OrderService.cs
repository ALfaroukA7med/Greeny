using AutoMapper;
using Greeny.BLL.Admin.Response;
using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.BLL.Admin.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepo orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<Order>>> GetAllAsync()
        {
            try
            {
                var orders = await _orderRepo.GetAllAsync();

                if (orders == null || !orders.Any())
                {
                    return new Response<IEnumerable<Order>>
                    {
                        IsSuccess = false,
                        Message = "No Orders Found"
                    };
                }

                return new Response<IEnumerable<Order>>
                {
                    IsSuccess = true,
                    Data = orders,
                    Message = "Orders Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Order>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<Order>> GetByIdAsync(int id)
        {
            try
            {
                var order = await _orderRepo.GetByIdAsync(id);

                if (order == null)
                {
                    return new Response<Order>
                    {
                        IsSuccess = false,
                        Message = "Order Not Found"
                    };
                }

                return new Response<Order>
                {
                    IsSuccess = true,
                    Data = order,
                    Message = "Order Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<Order>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<bool>> CreateAsync(Order order)
        {
            try
            {
                if (order == null)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        Message = "Invalid Data"
                    };
                }

                await _orderRepo.CreateAsync(order);

                return new Response<bool>
                {
                    IsSuccess = true,
                    Message = "Order Created Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<bool>> UpdateAsync(Order order)
        {
            try
            {
                var existing = await _orderRepo.GetByIdAsync(order.Id);

                if (existing == null)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        Message = "Order Not Found"
                    };
                }

                await _orderRepo.UpdateAsync(order);

                return new Response<bool>
                {
                    IsSuccess = true,
                    Message = "Order Updated Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<Response<bool>> DeleteAsync(int id)

        {
            try
            {
                var order = await _orderRepo.GetByIdAsync(id);

                if (order == null)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        Message = "Order Not Found"
                    };
                }
                await _orderRepo.DeleteAsync(id);
                return new Response<bool>
                {
                    IsSuccess = true,
                    Message = "Order Deleted Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<IEnumerable<Order>>> GetOrdersByUserIdAsync(string userId)
        {
            try
            {
                var orders = await _orderRepo.GetOrdersByUserIdAsync(userId);

                return new Response<IEnumerable<Order>>
                {
                    IsSuccess = true,
                    Data = orders,
                    Message = "Orders Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Order>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<IEnumerable<Order>>> GetOrdersByUserIdAndStatusAsync(string userId, string status)
        {
            try
            {
                var orders = await _orderRepo.GetOrdersByUserIdAndStatusAsync(userId, status);

                return new Response<IEnumerable<Order>>
                {
                    IsSuccess = true,
                    Data = orders,
                    Message = "Orders Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Order>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<IEnumerable<Order>>> GetOrdersByStatusAsync(string status)
        {
            try
            {
                var orders = await _orderRepo.GetOrdersByStatusAsync(status);

                return new Response<IEnumerable<Order>>
                {
                    IsSuccess = true,
                    Data = orders,
                    Message = "Orders Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Order>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<IEnumerable<Order>>> GetRecentOrdersAsync()
        {
            try
            {
                var orders = await _orderRepo.GetRecentOrdersAsync();

                return new Response<IEnumerable<Order>>
                {
                    IsSuccess = true,
                    Data = orders,
                    Message = "Recent Orders Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Order>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<bool>> ExistsAsync(int id)
        {
            try
            {
                var exists = await _orderRepo.ExistsAsync(id);

                return new Response<bool>
                {
                    IsSuccess = true,
                    Data = exists,
                    Message = exists ? "Order Exists" : "Order Not Found"
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<decimal>> GetTotalSalesAsync()
        {
            try
            {
                var total = await _orderRepo.GetTotalSalesAsync();

                return new Response<decimal>
                {
                    IsSuccess = true,
                    Data = total,
                    Message = "Total Sales Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<decimal>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}