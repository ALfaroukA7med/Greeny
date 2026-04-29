using AutoMapper;
using Greeny.BLL.Admin.Response;
using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.BLL.Admin.Services.Implementation
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepo _repo;
        private readonly IMapper _mapper;

        public OrderItemService(IOrderItemRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Response<bool>> CreateAsync(OrderItem orderItem)
        {
            try
            {
                if (orderItem == null)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        Message = "Invalid Data"
                    };
                }

                var result = await _repo.CreateAsync(orderItem);

                if (!result)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        Message = "Create Failed"
                    };
                }

                return new Response<bool>
                {
                    IsSuccess = true,
                    Message = "Created Successfully"
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

        public async Task<Response<IEnumerable<OrderItem>>> GetAllAsync()
        {
            try
            {
                var items = await _repo.GetAllAsync();

                if (items == null || !items.Any())
                {
                    return new Response<IEnumerable<OrderItem>>
                    {
                        IsSuccess = false,
                        Message = "No Data Found"
                    };
                }

                return new Response<IEnumerable<OrderItem>>
                {
                    IsSuccess = true,
                    Data = items,
                    Message = "Data Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<OrderItem>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<OrderItem?>> GetByIdAsync(int id)
        {
            try
            {
                var item = await _repo.GetByIdAsync(id);

                if (item == null)
                {
                    return new Response<OrderItem?>
                    {
                        IsSuccess = false,
                        Message = "Not Found"
                    };
                }

                return new Response<OrderItem?>
                {
                    IsSuccess = true,
                    Data = item,
                    Message = "Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderItem?>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<bool>> UpdateAsync(OrderItem newOrderItem)
        {
            try
            {
                var result = await _repo.UpdateAsync(newOrderItem);

                if (!result)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        Message = "Update Failed",
                        Data = false
                    };
                }

                return new Response<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = "Updated Successfully"
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
                var result = await _repo.DeleteAsync(id);

                return new Response<bool>
                {
                    IsSuccess = result,
                    Data = result,
                    Message = result ? "Deleted Successfully" : "Delete Failed"
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

        public async Task<Response<IEnumerable<OrderItem>>> GetByOrderIdAsync(int orderId)
        {
            try
            {
                var items = await _repo.GetByOrderIdAsync(orderId);

                return new Response<IEnumerable<OrderItem>>
                {
                    IsSuccess = true,
                    Data = items,
                    Message = "Data Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<OrderItem>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<decimal>> GetTotalPriceByOrderIdAsync(int orderId)
        {
            try
            {
                var total = await _repo.GetTotalPriceByOrderIdAsync(orderId);

                return new Response<decimal>
                {
                    IsSuccess = true,
                    Data = total,
                    Message = "Total Price Calculated"
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

        public async Task<Response<int>> GetItemsCountByOrderIdAsync(int orderId)
        {
            try
            {
                var count = await _repo.GetItemsCountByOrderIdAsync(orderId);

                return new Response<int>
                {
                    IsSuccess = true,
                    Data = count,
                    Message = "Count Retrieved"
                };
            }
            catch (Exception ex)
            {
                return new Response<int>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<bool>> ProductExistsInOrderAsync(int orderId, int productId)
        {
            try
            {
                var exists = await _repo.ProductExistsInOrderAsync(orderId, productId);

                return new Response<bool>
                {
                    IsSuccess = true,
                    Data = exists,
                    Message = exists ? "Exists" : "Not Found"
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

        public async Task<Response<bool>> RemoveProductFromOrderAsync(int orderId, int productId)
        {
            try
            {
                var result = await _repo.RemoveProductFromOrderAsync(orderId, productId);

                return new Response<bool>
                {
                    IsSuccess = result,
                    Data = result,
                    Message = result ? "Removed Successfully" : "Remove Failed"
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
    }
}