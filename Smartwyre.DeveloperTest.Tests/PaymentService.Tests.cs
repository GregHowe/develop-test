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
    public   void Test_identifyProduct_Successfully(string productIdentifier)
    {
        var productDataStore = new ProductDataStore(_fixture.dbMock);
        Product product = productDataStore.GetProduct(productIdentifier);

        Assert.NotNull(product);
    }


    [Theory]
    [InlineData("R1")]
    [InlineData("R2")]
    [InlineData("R3")]
    public void Test_identify_Rebase_Successfully(string rebateIdentifier)
    {
        var rebateDataStore = new RebateDataStore(_fixture.dbMock);
        Rebate rebate = rebateDataStore.GetRebate(rebateIdentifier);

        Assert.NotNull(rebate);

    }


    [Theory]
    [InlineData("P2", "R2", 1500)]
    public void TestValidate_Dont_Let_IncorrectType_Case_FixedCashAmount( string productIdentifier,string rebateIdentifier, decimal volume)
    {
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedCashAmount());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;

        var result = service.Calculate(input);

        Assert.False(result.Success);
    }

    [Theory]
    [InlineData("P1", "R3", 700)]
    public void TestValidate_Dont_Let_IncorrectType_CaseFixedRateRebate(string productIdentifier, string rebateIdentifier, decimal volume)
    {
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedRateRebate());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;

        var result = service.Calculate(input);

        Assert.False(result.Success);
    }

    [Theory]
    [InlineData("P3", "R2", 800)]
    public void TestValidate_Dont_Let_IncorrectType_CaseAmountPerUom(string productIdentifier, string rebateIdentifier, decimal volume)
    {
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new AmountPerUom());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;

        var result = service.Calculate(input);

        Assert.False(result.Success);
    }


    [Theory]
    [InlineData("P1", "R2", 5000)]
    public void TestProcess_Success_FixedCashAmount(string productIdentifier, string rebateIdentifier, decimal volume)
    {
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedCashAmount());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;

        var result = service.Calculate(input);

        Assert.True(result.Success);
    }

    [Theory]
    [InlineData("P3", "R2", 1000)]
    public void TestProcess_Success_FixedRateRebate(string productIdentifier, string rebateIdentifier, decimal volume)
    {
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedRateRebate());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;

        var result = service.Calculate(input);

        Assert.True(result.Success);
    }


    [Theory]
    [InlineData("P2", "R2", 1000)]
    public void TestProcess_Success_AmountPerUom(string productIdentifier, string rebateIdentifier, decimal volume)
    {
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new AmountPerUom());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = productIdentifier;
        input.RebateIdentifier = rebateIdentifier;
        input.Volume = volume;


        var result = service.Calculate(input);

        Assert.True(result.Success);
    }


}
