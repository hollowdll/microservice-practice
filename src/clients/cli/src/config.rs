/// Configuration for backend API gateway endpoints.
pub struct UrlConfig {
    pub api_gateway: &'static str,
}

impl UrlConfig {
    /// Builds a new URL config. Only one instance of this is needed.
    pub fn build() -> UrlConfig {
        // Hard code for now. Will be changed to env variables later.
        UrlConfig {
            api_gateway: "http://localhost:5058/api/v1",
        }
    }

    /// Returns the API endpoint to get all customers.
    pub fn get_all_customers(&self) -> String {
        format!("{}/customer", self.api_gateway)
    }

    /// Returns the API endpoint to get customer by id.
    pub fn get_customer_by_id(&self, id: i32) -> String {
        format!("{}/customer/{}", self.api_gateway, id)
    }
}