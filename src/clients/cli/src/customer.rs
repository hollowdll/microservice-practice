use serde::{
    Serialize,
    Deserialize,
};
use std::fmt::{self, Display};

/// Holds customer data.
#[derive(Deserialize)]
pub struct CustomerData {
    /// Customer id.
    pub id: i32,
    /// Customer first name.
    #[serde(rename = "firstName")]
    pub first_name: String,
    /// Customer last name.
    #[serde(rename = "lastName")]
    pub last_name: String,
    /// Customer email address.
    pub email: String,
    /// Date and time when created in ISO-8601 format.
    #[serde(rename = "createdAt")]
    pub created_at: String,
}

impl Display for CustomerData {
    fn fmt(&self, f: &mut fmt::Formatter<'_>) -> fmt::Result {
        write!(
            f,
            "{} {}, ID: {}\nEmail: {}\nAdded: {}",
            self.first_name,
            self.last_name,
            self.id,
            self.email,
            self.created_at
        )
    }
}

/// Used to create customers.
#[derive(Serialize)]
pub struct CustomerCreateData {
    /// Customer first name.
    #[serde(rename = "firstName")]
    pub first_name: String,
    /// Customer last name.
    #[serde(rename = "lastName")]
    pub last_name: String,
    /// Customer email address.
    pub email: String,
}

impl CustomerCreateData {
    pub fn new(first_name: &str, last_name: &str, email: &str) -> CustomerCreateData {
        CustomerCreateData {
            first_name: first_name.to_string(),
            last_name: last_name.to_string(),
            email: email.to_string(),
        }
    }
}