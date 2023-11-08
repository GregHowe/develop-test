using System;
using Xunit;
using Smartwyre.DeveloperTest.Model;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.DTO;
using Smartwyre.DeveloperTest.Repository;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.RebateIncentiveType;
using Moq;
using Smartwyre.DeveloperTest.dboContext;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using Smartwyre.DeveloperTest.Interfaces;
using System.Xml;
using System.Linq;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests : IClassFixture<MockData>
{
    MockData _fixture = new MockData();

    public PaymentServiceTests(MockData fixture)
    {
        _fixture = fixture;
    }


    [Theory]
    [InlineData("P3")]
    [InlineData("P2")]
    [InlineData("P1")]
    public   void When_Find_Product_Success(string productIdentifier)
    {
        //Arrange
        var productDataStore = new ProductDataStore(_fixture.dbMock);
        
        //Act
        Product product = productDataStore.GetProduct(productIdentifier);

        //Assert
        Assert.NotNull(product);
    }


    [Theory]
    [InlineData("R1")]
    [InlineData("R2")]
    [InlineData("R3")]
    public void When_Find_Rebate_Success(string rebateIdentifier)
    {
        //Arrange
        var rebateDataStore = new RebateDataStore(_fixture.dbMock);

        //Act
        Rebate rebate = rebateDataStore.GetRebate(rebateIdentifier);

        //Assert
        Assert.NotNull(rebate);

    }

    [Theory]
    [InlineData("P22", "R22", 900)]
    public void When_Calculation_Failed_because_Product_Doesnt_Exist_ERROR(string productIdentifier, string rebateIdentifier, decimal volume)
    {
        //Arrange
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedCashAmount());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;

        //Act && Assert
        Assert.Throws<DontFindElementException>(() => service.Calculate(input));

    }


    [Theory]
    [InlineData("P2", "R2", 1500)]
    public void When_Calculation_Result_Is_False_Because_Product_Is_Not_Case_FixedCashAmount( string productIdentifier,string rebateIdentifier, decimal volume)
    {
        //Arrange
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedCashAmount());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;

        //Act
        var result = service.Calculate(input);

        //Assert
        Assert.False(result.Success);
    }

    [Theory]
    [InlineData("P1", "R3", 700)]
    public void When_Calculation_Result_Is_False_Because_Product_Is_Not_Case_FixedRateRebate(string productIdentifier, string rebateIdentifier, decimal volume)
    {
        //Arrange
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedRateRebate());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;

        //Act
        var result = service.Calculate(input);

        //Assert
        Assert.False(result.Success);
    }

    [Theory]
    [InlineData("P3", "R2", 800)]
    public void When_Calculation_Result_Is_False_Because_Product_Is_Not_Case_CaseAmountPerUom(string productIdentifier, string rebateIdentifier, decimal volume)
    {
        //Arrange
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new AmountPerUom());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;

        //Act
        var result = service.Calculate(input);

        //Assert
        Assert.False(result.Success);
    }


    [Theory]
    [InlineData("P1", "R2", 5000)]
    public void When_Calculation_Result_Is_Sucess_Case_FixedCashAmount(string productIdentifier, string rebateIdentifier, decimal volume)
    {
        //Arrange
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedCashAmount());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;

        //Act
        var result = service.Calculate(input);

        //Assert
        Assert.True(result.Success);
    }

    [Theory]
    [InlineData("P3", "R2", 1000)]
    public void When_Calculation_Result_Is_Sucess_Case_FixedRateRebate(string productIdentifier, string rebateIdentifier, decimal volume)
    {
        //Arrange
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedRateRebate());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;

        //Act
        var result = service.Calculate(input);

        //Assert
        Assert.True(result.Success);
    }


    [Theory]
    [InlineData("P2", "R2", 1000)]
    public void When_Calculation_Result_Is_Sucess_Case_AmountPerUom(string productIdentifier, string rebateIdentifier, decimal volume)
    {
        //Arrange
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new AmountPerUom());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;

        //Act
        var result = service.Calculate(input);

        //Assert
        Assert.True(result.Success);
    }


}
