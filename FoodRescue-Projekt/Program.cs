using DataLayer.Backend;
using FoodRescue_Projekt;

var adminBackend = new AdminBackend();
var customerclient = new CustomerClient();

adminBackend.PrepDatabase();

customerclient.client();