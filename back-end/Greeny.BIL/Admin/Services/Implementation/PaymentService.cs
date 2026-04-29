using AutoMapper;
using Greeny.BLL.Admin.Response;
using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.BLL.Admin.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo _repo;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Response<bool>> CreateAsync(Payment payment)
        {
            try
            {
                if (payment == null)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        Message = "Invalid Data"
                    };
                }
                var result = await _repo.CreateAsync(payment);

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
                    Message = "Payment Created Successfully"
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
        public async Task<Response<IEnumerable<Payment>>> GetAllAsync()
        {
            try
            {
                var payments = await _repo.GetAllAsync();

                if (payments == null || !payments.Any())
                {
                    return new Response<IEnumerable<Payment>>
                    {
                        IsSuccess = false,
                        Message = "No Payments Found"
                    };
                }

                return new Response<IEnumerable<Payment>>
                {
                    IsSuccess = true,
                    Data = payments,
                    Message = "Payments Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Payment>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<Payment?>> GetByIdAsync(int id)
        {
            try
            {
                var payment = await _repo.GetByIdAsync(id);

                if (payment == null)
                {
                    return new Response<Payment?>
                    {
                        IsSuccess = false,
                        Message = "Payment Not Found"
                    };
                }

                return new Response<Payment?>
                {
                    IsSuccess = true,
                    Data = payment,
                    Message = "Payment Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<Payment?>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<bool>> UpdateAsync(Payment newPayment)
        {
            try
            {
                var result = await _repo.UpdateAsync(newPayment);

                return new Response<bool>
                {
                    IsSuccess = result,
                    Data = result,
                    Message = result ? "Updated Successfully" : "Update Failed"
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

        public async Task<Response<IEnumerable<Payment>>> GetByUserIdAsync(string userId)
        {
            try
            {
                var payments = await _repo.GetByUserIdAsync(userId);

                return new Response<IEnumerable<Payment>>
                {
                    IsSuccess = true,
                    Data = payments,
                    Message = "Payments Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Payment>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<Payment?>> GetByOrderIdAsync(int orderId)
        {
            try
            {
                var payment = await _repo.GetByOrderIdAsync(orderId);

                if (payment == null)
                {
                    return new Response<Payment?>
                    {
                        IsSuccess = false,
                        Message = "Not Found"
                    };
                }

                return new Response<Payment?>
                {
                    IsSuccess = true,
                    Data = payment,
                    Message = "Payment Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<Payment?>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<bool>> ExistsByOrderIdAsync(int orderId)
        {
            try
            {
                var exists = await _repo.ExistsByOrderIdAsync(orderId);

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

        public async Task<Response<IEnumerable<Payment>>> GetAllByStatusAsync(string status)
        {
            try
            {
                var payments = await _repo.GetAllByStatusAsync(status);

                if (payments == null || !payments.Any())
                {
                    return new Response<IEnumerable<Payment>>
                    {
                        IsSuccess = false,
                        Message = "No Payments Found"
                    };
                }

                return new Response<IEnumerable<Payment>>
                {
                    IsSuccess = true,
                    Data = payments,
                    Message = "Payments Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Payment>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}