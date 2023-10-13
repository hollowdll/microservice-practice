use reqwest::Client;
use crate::{
    config::UrlConfig,
    customer::{
        CustomerData,
        CustomerCreateData,
    },
    ticket::{
        TicketData,
        TicketCreateData,
    },
};
use std::error::Error;

/// HTTP client used for network requests to the microservice-practice backend system.
pub struct HttpClient {
    pub client: Client,
    pub url_config: UrlConfig,
}

impl HttpClient {
    /// Builds a new HTTP client. Only one instance of this is needed.
    pub fn build() -> HttpClient {
        HttpClient {
            client: Client::new(),
            url_config: UrlConfig::build(),
        }
    }

    /// Sends HTTP GET request to the API gateway to get all customers.
    /// 
    /// Returns the customers or any error that may occur.
    pub async fn get_all_customers(&self) -> Result<Vec<CustomerData>, Box<dyn Error>> {
        let response = self.client
            .get(self.url_config.get_all_customers())
            .send()
            .await?;
        
        if !response.status().is_success() {
            return Err(format!("{}", response.status()).into());
        }
        let customers = response.json::<Vec<CustomerData>>().await?;

        Ok(customers)
    }

    /// Sends HTTP GET request to the API gateway to get customer by id.
    /// 
    /// Returns the customer if it was found or any error that may occur.
    pub async fn get_customer_by_id(&self, id: i32) -> Result<CustomerData, Box<dyn Error>> {
        let response = self.client
            .get(self.url_config.get_customer_by_id(id))
            .send()
            .await?;

        if !response.status().is_success() {
            return Err(format!("{}", response.status()).into());
        }
        let customer = response.json::<CustomerData>().await?;

        Ok(customer)
    }

    /// Sends HTTP POST request to the API gateway to add a customer.
    /// 
    /// Returns Ok(()) if successful or any error that may occur.
    pub async fn add_customer(&self, customer: &CustomerCreateData) -> Result<CustomerData, Box<dyn Error>> {
        let response = self.client
            .post(self.url_config.add_customer())
            .json(customer)
            .send()
            .await?;

        if !response.status().is_success() {
            return Err(format!("{}", response.status()).into());
        }
        let customer = response.json::<CustomerData>().await?;

        Ok(customer)
    }

    /// Sends HTTP GET request to the API gateway to get all tickets.
    /// 
    /// Returns the tickets or any error that may occur.
    pub async fn get_all_tickets(&self) -> Result<Vec<TicketData>, Box<dyn Error>> {
        let response = self.client
            .get(self.url_config.get_all_tickets())
            .send()
            .await?;
        
        if !response.status().is_success() {
            return Err(format!("{}", response.status()).into());
        }
        let tickets = response.json::<Vec<TicketData>>().await?;

        Ok(tickets)
    }
}