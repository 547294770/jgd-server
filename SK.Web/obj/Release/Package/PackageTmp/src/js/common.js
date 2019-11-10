var htmlFunction = {};
var JGD = {
    
};

htmlFunction.status = function (status,deltype,picktype) {
    if (status === 'ConfirmDeliveryMethod') return GlobalSettings.enums["SK.Entities.DeliveryOrder+OrderType"][deltype];
    //if (status === 'ConfirmDeliveryMethod') return GlobalSettings.enums["SK.Entities.DeliveryOrder+OrderType"][deltype];

    return GlobalSettings.enums["SK.Entities.ProcessingOrder+OrderStatus"][status];
}