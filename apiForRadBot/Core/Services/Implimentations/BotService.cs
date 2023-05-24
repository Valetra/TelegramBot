﻿using apiForRadBot.Core.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using apiForRadBot.Data.Models;
using apiForRadBot.Data.Repositories.Interfaces;
using apiForRadBot.Data.RequestObject;
using apiForRadBot.Core.Mapper;
using apiForRadBot.Data.Repositories.Implimentations;

namespace apiForRadBot.Core.Services.Implimentations;

public class BotService : IBotService
{
    private readonly ISupplyRepository _supplyRepository;
    private readonly IOrderRepository _orderRepository;

    public BotService(ISupplyRepository supplyRepository, IOrderRepository orderRepository)
    {
        _supplyRepository = supplyRepository;
        _orderRepository = orderRepository;
    }

    //Supply processing
    public async Task<IEnumerable<Supply>> GetAllSupplies() => await _supplyRepository.GetAllSupplies();
    public async Task<Supply?> GetSupply(Guid id) => await _supplyRepository.GetSupply(id);
    public async Task<Supply> AddSupply(Supply supply) => await _supplyRepository.CreateSupply(supply);
    public async Task<Supply> UpdateSupply(Supply supply) => await _supplyRepository.UpdateSupply(supply);
    public async Task DeleteSupply(Guid id) => await _supplyRepository.DeleteSupply(id);


    //Order processing
    public async Task<IEnumerable<Order>> GetAllOrders() => await _orderRepository.GetAll();
    public async Task<Order?> GetOrder(Guid id) => await _orderRepository.Get(id);
    public async Task<Order> AddOrder(Order order) => await _orderRepository.Create(order);
    public async Task<Order> UpdateOrder(Order order) => await _orderRepository.Update(order);
    public async Task DeleteOrder(Guid id) => await _orderRepository.Delete(id);
    public async Task<Order> ChangeOrderStatus(Order order, string status)
    {
        order.Status = status;
        return await _orderRepository.Update(order);
    }
    public async Task<Order> OrderGotPayed(Order order)
    {
        order.Payed = true;
        return await _orderRepository.Update(order);
    }
}
