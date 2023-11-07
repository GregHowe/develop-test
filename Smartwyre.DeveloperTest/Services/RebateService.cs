using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.DTO;
using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Model;
using Smartwyre.DeveloperTest.RebateIncentiveType;
namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    IProduct iProduct;
    IRebate iRebate;
    IRebateCalculation iRebateCalculation;
    IRebateBase iRebateBase;

    public RebateService(
            IProduct _iProduct,
            IRebate _iRebate,
            IRebateCalculation _iRebateCalculation,
            IRebateBase _iRebateBase)
    {
        iProduct = _iProduct;
        iRebate = _iRebate;
        iRebateCalculation = _iRebateCalculation;
        iRebateBase = _iRebateBase;
    }

    private bool validationNull(Product product, Rebate rebate)
    {
        bool result = false;
        if (product != null && rebate != null)
        {
            result = true;
        }
        return result;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Product product = iProduct.GetProduct(request.ProductIdentifier);
        Rebate rebate = iRebate.GetRebate(request.RebateIdentifier);

        var result = new CalculateRebateResult();
        var validNull = validationNull(product, rebate);
        if (!validNull)
            return result;
        
        var rebateAmount = 0m;

        result.Success = iRebateBase.validate(product, rebate, request);
        if (result.Success)
        {
            rebateAmount = iRebateBase.calculateRebateAmount(product, rebate, request);
            rebate.Amount = rebateAmount;
            StoreCalculationResult(rebate);
        }

        return result;
    }

    public void StoreCalculationResult(Rebate account)
    {
        iRebateCalculation.StoreCalculationResult(account);
    }

}
