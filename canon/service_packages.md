Package 1: Keon Runtime
For customers who already have AI workflows and need governance.

Includes:

MCP Gateway
governed execute
policy decisions
receipts
audit trail
quotas / tenant controls
This is the lowest-friction entry point.

Package 2: Keon Runtime + Cortex
For customers who need durable AI memory.

Includes:

all Runtime capabilities
governed memory write/read
receipt lookup
lineage
replay foundations
This is the serious enterprise tier.

Package 3: Keon Runtime + Cortex + Collective
For customers who want governed cognition.

Includes:

all Runtime
all Cortex
Collective deliberation
adversarial review
collapse
governed handoff
cognition inspection
This is the flagship product.

I would not sell Collective alone.

Suggested Rule
Runtime can stand alone.

Cortex requires Runtime or includes Runtime governance internally.

Collective requires Runtime and Cortex.

That feels right.

Why This Is Strong Commercially
This gives Keon a natural adoption ladder:

Step 1
“Govern my existing AI actions.”

Runtime.

Step 2
“Give my AI governed memory.”

Cortex.

Step 3
“Give my AI governed cognition.”

Collective.

That is a clean story.

It also maps to increasing maturity and increasing contract value.

Public Positioning

"Keon exposes governance, memory, and cognition as MCP-native services. Every service uses the same tenant, actor, policy, scope, and receipt model. Runtime governs effects. Cortex preserves truth. Collective produces auditable cognition. Customers may adopt Runtime first, add Cortex for memory, and unlock Collective when they are ready for governed cognition."

Expose all three as MCP services.

But enforce these invariants:

One access model across all products.
One governed MCP edge.
Plane-specific tools.
Runtime remains the only execution authority.
Cortex memory writes are governed and receipted.
Collective cannot be used without Runtime and Cortex.
Collective produces handoffs, not effects.
All client-visible outcomes carry receipts or fail closed.
If we do that, MCP becomes not just an integration protocol for Keon.

It becomes the front door to the Keon operating model.