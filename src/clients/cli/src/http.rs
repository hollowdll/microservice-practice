use reqwest::Client;
use crate::{
    config::UrlConfig,
    customer::CustomerData
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
        let customers = self.client
            .get(self.url_config.get_all_customers())
            .send()
            .await?
            .json::<Vec<CustomerData>>()
            .await?;

        Ok(customers)
    }
}