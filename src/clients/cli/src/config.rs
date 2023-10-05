/// Configuration for backend API gateway endpoints.
pub struct UrlConfig {
    pub get_all_customers: &'static str,
}

impl UrlConfig {
    pub fn build() -> UrlConfig {
        // Hard code for now. Will be changed to env variables later.
        UrlConfig {
            get_all_customers: "http://localhost:5058/api/v1/customer",
        }
    }
}