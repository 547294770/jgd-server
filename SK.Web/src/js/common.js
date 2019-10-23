var htmlFunction = {};
var JGD = {
    
};

htmlFunction.status = function (status) {
    return GlobalSettings.enums["SK.Entities.ProcessingOrder+OrderStatus"][status];
}