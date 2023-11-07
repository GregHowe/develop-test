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

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests : IClassFixture<TestDataHelper>
{
    TestDataHelper _fixture = new TestDataHelper();

    public PaymentServiceTests(TestDataHelper fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public   void Test_identifyProduct_Successfully()
    {

        //var testContext = new TestContext();
        //testContext.GetMockProducts();

        //var _contextMock = new Mock<dbContext>();
        //_contextMock.Setup<DbSet<Product>>(x => x.Products)
        //.ReturnsDbSet(TestDataHelper.GetFakeProductList());

        //_contextMock.Setup<DbSet<Product>>(x => x.Products).ReturnsDbSet(TestDataHelper.GetFakeProductList());
        //.ReturnsDbSet(TestDataHelper.GetFakeEmployeeList());

        //var _product = new Product
        //{
        //    Id = 1,
        //    Identifier = "P1",
        //    Price = 1,
        //    Uom = "Feet",
        //    SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        //};
        //Mock<IProduct> mockIProduct = new Mock<IProduct>();
        //mockIProduct.Setup(p => p.GetProduct(It.IsAny<string>())).Returns(_product);

        //-------------------------------------------------------------------------------

        var productIdentifier = "P1";

        var productDataStore = new ProductDataStore(_fixture.dbMock);
        Product product = productDataStore.GetProduct(productIdentifier);

        Assert.NotNull(product);
    }


    [Fact]
    public void Test_identify_Rebase_Successfully()
    {
        var rebateIdentifier = "R1";

        var rebateDataStore = new RebateDataStore(_fixture.dbMock);
        Rebate rebate = rebateDataStore.GetRebate(rebateIdentifier);

        Assert.NotNull(rebate);

    }

    [Fact]
    public void TestValidate_Dont_Let_IncorrectType_Case_FixedCashAmount()
    {
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedCashAmount());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = "P2";
        input.RebateIdentifier = "R2";
        input.Volume = 1500;

        var result = service.Calculate(input);

        Assert.False(result.Success);
    }

    [Fact]
    public void TestValidate_Dont_Let_IncorrectType_CaseFixedRateRebate()
    {
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedRateRebate());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = "P1";
        input.RebateIdentifier = "R3";
        input.Volume = 1500;

        var result = service.Calculate(input);

        Assert.False(result.Success);
    }

    [Fact]
    public void TestValidate_Dont_Let_IncorrectType_CaseAmountPerUom()
    {
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new AmountPerUom());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = "P3";
        input.RebateIdentifier = "R2";
        input.Volume = 1500;

        var result = service.Calculate(input);

        Assert.False(result.Success);
    }


    [Fact]
    public void TestProcess_Success_FixedCashAmount()
    {
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedCashAmount());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = "P1";
        input.RebateIdentifier = "R2";
        input.Volume = 1500;

        var result = service.Calculate(input);

        Assert.True(result.Success);
    }

    [Fact]
    public void TestProcess_Success_FixedRateRebate()
    {
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new FixedRateRebate());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = "P3";
        input.RebateIdentifier = "R2";
        input.Volume = 1500;

        var result = service.Calculate(input);

        Assert.True(result.Success);
    }


    [Fact]
    public void TestProcess_Success_AmountPerUom()
    {
        var service = new RebateService(new ProductDataStore(_fixture.dbMock), new RebateDataStore(_fixture.dbMock), new CalculationDataStore(_fixture.dbMock), new AmountPerUom());

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = "P2";
        input.RebateIdentifier = "R2";
        input.Volume = 1500;

        var result = service.Calculate(input);

        Assert.True(result.Success);
    }


}
