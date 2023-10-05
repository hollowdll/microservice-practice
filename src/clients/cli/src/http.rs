use reqwest::Client;
use crate::config::UrlConfig;

/// HTTP client used for network requests to the backend system.
pub struct HttpClient {
    pub client: Client,
    pub url_config: UrlConfig,
}

impl HttpClient {
    pub fn build() -> HttpClient {
        HttpClient {
            client: Client::new(),
            url_config: UrlConfig::build(),
        }
    }
}