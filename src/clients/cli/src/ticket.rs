use serde::{
    Serialize,
    Deserialize,
};

/// Holds ticket data.
#[derive(Deserialize)]
pub struct TicketData {
    /// Ticket id.
    pub id: i32,
    /// Ticket code.
    pub code: String,
    /// Ticket message.
    pub message: String,
    /// Customer id.
    pub customer_id: i32,
    /// Date and time when created in ISO-8601 format.
    #[serde(rename = "createdAt")]
    pub created_at: String,
}

/// Used to create tickets.
#[derive(Serialize)]
pub struct TicketCreateData {
    /// Customer id.
    #[serde(rename = "customerId")]
    pub customer_id: i32,
}